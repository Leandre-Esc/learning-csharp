using LCS.Infra;
using LCS.Infra.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --- Services -----------------------------------------------------

var connectionString = builder.Configuration.GetConnectionString("Postgres")
                       ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

builder.Services.AddInfraServices(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- Build --------------------------------------------------------
var app = builder.Build();

// --- Migration ----------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var migrator = scope.ServiceProvider.GetRequiredService<DatabaseMigrator>();
    await migrator.MigrateAsync();
}

// --- Middleware pipeline ------------------------------------------

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();