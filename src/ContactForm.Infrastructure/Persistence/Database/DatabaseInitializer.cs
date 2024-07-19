using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ContactForm.Infrastructure.Persistence.Database;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DatabaseInitializer> logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        const string sectionName = "Database";
        IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        string connectionString = configuration.GetConnectionString(sectionName)!;

        try
        {
            EnsureDatabase.For.SqlDatabase(connectionString);

            UpgradeEngine? upgradeEngine = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransaction()
                .LogToConsole()
                .Build();

            DatabaseUpgradeResult? result = upgradeEngine.PerformUpgrade();

            if (!result.Successful)
            {
                logger.LogError("Failed migrating database. Error: {Error}", result.Error);
                throw new InvalidOperationException("Failed migrating database.");
            }

            logger.LogInformation("Database has been successfully migrated.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}