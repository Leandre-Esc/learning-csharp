using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LCS.Infra.Persistence;

public class DatabaseMigrator(AppDbContext dbContext, ILogger<DatabaseMigrator> logger)
{
    public async Task MigrateAsync(CancellationToken ct = default)
    {
        try
        {
            logger.LogInformation("Checking for pending database migrations...");

            await dbContext.Database.EnsureCreatedAsync(ct);

            var pendingMigrations = await dbContext.Database
                .GetPendingMigrationsAsync(ct);

            var pending = pendingMigrations.ToList();

            if (pending.Count == 0)
            {
                logger.LogInformation("Database is up to date. No migrations needed.");
                return;
            }

            logger.LogInformation(
                "Applying {Count} pending migrations: {Migrations}",
                pending.Count,
                string.Join(", ", pending));

            await dbContext.Database.MigrateAsync(ct);

            logger.LogInformation("All migrations applied successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }
}