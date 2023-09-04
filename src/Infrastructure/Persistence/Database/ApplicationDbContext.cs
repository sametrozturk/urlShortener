using Application.Common;
using Domain.ResponseHandler;
using Domain.ShortenedUrl;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<ResponseMessage> ResponseMessages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }


}