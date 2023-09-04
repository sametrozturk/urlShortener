using Application.Common;
using Application.Exceptions;
using Application.Shared;
using Application.ShortenUrl.Models;
using Domain.ShortenedUrl;
using Microsoft.EntityFrameworkCore;

namespace Application.ShortenUrl.Services;

public class ShortenUrlService : IShortenUrlService
{
    private readonly Random _random = new Random();
    private readonly IApplicationDbContext _context;
    private readonly IUrlValidatorService _urlValidator;

    public ShortenUrlService(IApplicationDbContext context, IUrlValidatorService urlValidator)
    {
        this._context = context ?? throw new ArgumentNullException(nameof(context));
        this._urlValidator = urlValidator ?? throw new ArgumentNullException(nameof(urlValidator));
    }

    public async Task<Tuple<bool, ShortenedUrl?>> IsCustomUrlExist(string host, string customUrl)
    {
        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(customUrl))
            throw new InvalidOperationException("Parameters must not be null");

        var result = await this._context.ShortenedUrls
            .Where(x => x.ShortUrl == customUrl)
            .ToListAsync();

        bool isHostBelongsToDiffUrl = result.Any(x => x.GivenUrlHost == host && x.ShortUrl != customUrl);
        bool isCustomUrlBelongsToDiffHost = result.Any(x => x.GivenUrlHost != host && x.ShortUrl == customUrl);
        bool isCustomHostUrlFound = result.Any(x => x.GivenUrlHost == host && x.ShortUrl == customUrl);

        if ((isHostBelongsToDiffUrl || isCustomUrlBelongsToDiffHost) && !isCustomHostUrlFound)
            throw new UrlShortenerExceptions(CustomErrorCodes.HASH_ALREADY_IN_USE);

        var foundUrl = result.FirstOrDefault(x => x.GivenUrlHost == host && x.ShortUrl == customUrl);

        return Tuple.Create(foundUrl != null, foundUrl);
    }

    public SeperateHostAndRouteDto SeperateHostAndRoute(string url)
    {
        if (!_urlValidator.IsUrlValid(url))
            throw new UrlShortenerExceptions(CustomErrorCodes.INVALID_URL);

        Uri uri = new Uri(url);

        string host = uri.Host;
        string scheme = uri.Scheme;
        string route = uri.AbsolutePath;

        return new SeperateHostAndRouteDto
        {
            Host = host,
            Scheme = scheme,
            Route = route
        };
    }

    public async Task<string> GenerateHash()
    {
        char[] hash = new char[ShortenedUrlConfig.Maxlength];

        while (true)
        {
            for (int i = 0; i < ShortenedUrlConfig.Maxlength; i++)
            {
                int randomIndex = this._random.Next(ShortenedUrlConfig.Maxlength - 1);
                hash[i] = ShortenedUrlConfig.Alphabet[randomIndex];
            }

            string code = new string(hash);

            if (await IsHashSuitable(code))
                return code;
        }
    }

    private async Task<bool> IsHashSuitable(string hash)
    {
        if (string.IsNullOrEmpty(hash))
            throw new UrlShortenerExceptions(CustomErrorCodes.INVALID_HASH);

        return !await this._context.ShortenedUrls.AnyAsync(x => x.Hash == hash);
    }
}