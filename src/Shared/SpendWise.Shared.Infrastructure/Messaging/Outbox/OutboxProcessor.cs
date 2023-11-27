using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SpendWise.Shared.Infrastructure.Messaging.Outbox;

internal sealed class OutboxProcessor(IServiceScopeFactory serviceScopeFactory, OutboxOptions outboxOptions,
        ILogger<OutboxProcessor> logger)
    : BackgroundService
{
    private readonly TimeSpan _interval = outboxOptions.Interval ?? TimeSpan.FromSeconds(1);
    private readonly bool _enabled = outboxOptions.Enabled;
    private readonly TimeSpan _startDelay = outboxOptions.StartDelay ?? TimeSpan.FromSeconds(5);
    private int _isProcessing;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_enabled)
        {
            logger.LogWarning("Outbox is disabled");
            return;
        }

        logger.LogInformation($"Outbox is enabled, start delay: {_startDelay}, interval: {_interval}");
        await Task.Delay(_startDelay, stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            if (Interlocked.Exchange(ref _isProcessing, 1) == 1)
            {
                await Task.Delay(_interval, stoppingToken);
                continue;
            }
                
            logger.LogTrace("Started processing outbox messages...");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                try
                {
                    var outboxes = scope.ServiceProvider.GetServices<IOutbox>();
                    var tasks = outboxes.Select(outbox => outbox.PublishUnsentAsync());
                    await Task.WhenAll(tasks);
                }
                catch (Exception exception)
                {
                    logger.LogError("There was an error when processing outbox.");
                    logger.LogError(exception, exception.Message);
                }
                finally
                {
                    Interlocked.Exchange(ref _isProcessing, 0);
                    stopwatch.Stop();
                    logger.LogTrace($"Finished processing outbox messages in {stopwatch.ElapsedMilliseconds} ms.");
                }
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}