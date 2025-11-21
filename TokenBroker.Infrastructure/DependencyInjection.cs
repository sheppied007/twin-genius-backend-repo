using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TokenBroker.Application.Interfaces;
using TokenBroker.Infrastructure.Implementations;

namespace TokenBroker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
