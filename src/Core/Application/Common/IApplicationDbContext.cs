using Domain.ResponseHandler;
using Domain.ShortenedUrl;
using Microsoft.EntityFrameworkCore;

namespace Application.Common;

public interface IApplicationDbContext
{

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<ResponseMessage> ResponseMessages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
