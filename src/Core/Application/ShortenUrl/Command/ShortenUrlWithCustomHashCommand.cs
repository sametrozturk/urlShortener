using Application.Common;
using Domain.ShortenedUrl;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ShortenUrl.Command;

public class ShortenUrlWithCustomHashCommand : IRequest<UrlShortenerResult>
{
    public string Url { get; set; }
    public string CustomUrl { get; set; }

    public ShortenUrlWithCustomHashCommand(string url, string customUrl)
    {
        this.Url = url;
        this.CustomUrl = customUrl;
    }

    public class ShortenUrlWithCustomHashCommandHandler : IRequestHandler<ShortenUrlWithCustomHashCommand, UrlShortenerResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShortenUrlService _shortenUrlService;

        public ShortenUrlWithCustomHashCommandHandler(
            IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IShortenUrlService shortenUrlService)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._shortenUrlService = shortenUrlService;
        }
        public async Task<UrlShortenerResult> Handle(ShortenUrlWithCustomHashCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;


            var httpRequest = httpContext.Request;
            var host = httpRequest.Host;
            var scheme = httpRequest.Scheme;
            var hostUrl = $"{scheme}://{host}";

            var customHash = request.CustomUrl;
            var shortUrl = $"{hostUrl}/api/{customHash}";

            var sepereatedUrl = this._shortenUrlService.SeperateHostAndRoute(request.Url);

            var existedUrl = await this._shortenUrlService.IsCustomUrlExist(sepereatedUrl.Host, shortUrl);

            if (existedUrl.Item1)
            {
                return new UrlShortenerResult(200, existedUrl.Item2.ShortUrl);
            }

            var shortenedUrl = ShortenedUrl.CreateNew(
                hostUrl,
                request.Url,
                sepereatedUrl.Scheme,
                sepereatedUrl.Host,
                sepereatedUrl.Route,
                shortUrl,
                true,
                customHash);

            this._context.ShortenedUrls.Add(shortenedUrl);
            await this._context.SaveChangesAsync(cancellationToken);

            return new UrlShortenerResult(200, shortenedUrl.ShortUrl);
        }

    }

}

