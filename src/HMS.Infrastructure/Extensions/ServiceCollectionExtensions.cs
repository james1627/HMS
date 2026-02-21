using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HMS.Infrastructure.Services;

namespace HMS.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration config)
    {
        var apiBase = config["ApiSettings:ApiBaseUrl"] ?? "https://localhost:8080/";
        services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri(apiBase);
        });
        services.AddScoped<AuthService>();
        return services;
    }
}