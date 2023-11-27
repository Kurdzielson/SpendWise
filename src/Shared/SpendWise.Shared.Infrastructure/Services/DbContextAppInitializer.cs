using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SpendWise.Shared.Infrastructure.Services;

public class DbContextAppInitializer(IServiceProvider serviceProvider, ILogger<DbContextAppInitializer> logger)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        using var scope = serviceProvider.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
            if (dbContext is null)
            {
                continue;
            }

            logger.LogInformation("Running DB context for module {Module}...", dbContextType.GetModuleName());
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        var initializers = scope.ServiceProvider.GetServices<IInitializer>();
        foreach (var initializer in initializers)
        {
            try
            {
                logger.LogInformation($"Running the initializer: {initializer.GetType().Name}...");
                await initializer.InitAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}