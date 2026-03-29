using LCS.Domain.Interfaces;
using LCS.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LCS.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(
        this IServiceCollection services)
    {
        services.AddScoped<IEnvironmentService, EnvironmentService>();

        return services;
    }
}