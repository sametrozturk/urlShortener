using Application.Common;
using Application.ShortenUrl.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application;

public static class ServiceRegistration
{

    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        serviceCollection.AddHttpContextAccessor();

        serviceCollection.AddScoped<IShortenUrlService, ShortenUrlService>();
        serviceCollection.AddScoped<IUrlValidatorService, UrlValidatorService>();
    }

}