using Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Database;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Configure SQLite database
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                );
            });

            // Register the interface with the DbContext
            serviceCollection.AddScoped(provider =>
            {
                var dbContext = provider.GetService<ApplicationDbContext>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("ApplicationDbContext is null.");
                }
                return (IApplicationDbContext)dbContext;
            });


        }
    }
}