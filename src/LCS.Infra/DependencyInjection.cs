using LCS.Domain.Repositories;
using LCS.Infra.Persistence;
using LCS.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LCS.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseNpgsql(
                connectionString,
                npgsql => npgsql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        services.AddScoped<DatabaseMigrator>();
        services.AddScoped<IUserRepository, UserRepository>();
        // services.AddSingleton<IEnvironmentService, EnvironmentService>();

        return services;
    }
}