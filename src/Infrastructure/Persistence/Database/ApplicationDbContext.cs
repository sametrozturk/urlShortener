

using Application.Common;
using Application.Shared;
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

        modelBuilder.Entity<ShortenedUrl>(builder =>
        {
            builder.Property(s => s.Hash).HasMaxLength(ShortenedUrlConfig.Maxlength);
            builder.Property(s => s.Hash).UseCollation("Latin1_General_CS_AS");
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }


}
