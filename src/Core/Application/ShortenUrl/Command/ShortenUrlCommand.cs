using Application.Common;
using Domain.ShortenedUrl;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.ShortenUrl.Command;

public class ShortenUrlCommand : IRequest<UrlShortenerResult>
{
    public string Url { get; set; }

    public ShortenUrlCommand(string url)
    {
        this.Url = url;
    }

    public class ShortenUrlCommandHandler : IRequestHandler<ShortenUrlCommand, UrlShortenerResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShortenUrlService _shortenUrlService;

        public ShortenUrlCommandHandler(
            IApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IShortenUrlService shortenUrlService)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._shortenUrlService = shortenUrlService;
        }
        public async Task<UrlShortenerResult> Handle(ShortenUrlCommand request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var httpRequest = httpContext.Request;
            var host = httpRequest.Host;
            var scheme = httpRequest.Scheme;
            var hostUrl = $"{scheme}://{host}";

            var sepereatedUrl = this._shortenUrlService.SeperateHostAndRoute(request.Url);
            var hash = await this._shortenUrlService.GenerateHash();

            var shortUrl = $"{hostUrl}/api/{hash}";

            var shortenedUrl = ShortenedUrl.CreateNew(
                hostUrl,
                request.Url,
                sepereatedUrl.Scheme,
                sepereatedUrl.Host,
                sepereatedUrl.Route,
                shortUrl,
                false,
                hash);

            this._context.ShortenedUrls.Add(shortenedUrl);
            await this._context.SaveChangesAsync(cancellationToken);

            return new UrlShortenerResult(200, shortenedUrl.ShortUrl);
        }

    }

}

