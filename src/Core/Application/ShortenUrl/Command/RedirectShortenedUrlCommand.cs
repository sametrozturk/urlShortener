using Application.Common;
using Application.Exceptions;
using Application.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShortenUrl.Command;

public class RedirectShortenedUrlCommand : IRequest<UrlShortenerResult>
{
    public string Hash { get; set; }

    public RedirectShortenedUrlCommand(string hash)
    {
        this.Hash = hash;
    }

    public class RedirectShortenedUrlCommandHandler : IRequestHandler<RedirectShortenedUrlCommand, UrlShortenerResult>
    {
        private readonly IApplicationDbContext _context;

        public RedirectShortenedUrlCommandHandler(IApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<UrlShortenerResult> Handle(RedirectShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await this._context.ShortenedUrls
                .AsNoTracking()
                .Where(x => x.Hash == request.Hash)
                .FirstOrDefaultAsync(cancellationToken);

            if (result == null)
            {
                throw new UrlShortenerExceptions(CustomErrorCodes.RECORD_NOT_FOUND);
            }

            return new UrlShortenerResult(200, result.GivenUrl);
        }

    }

}