using LCS.Application.Abstractions;
using LCS.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LCS.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IEnvironmentService, EnvironmentService>();

        return services;
    }
}
