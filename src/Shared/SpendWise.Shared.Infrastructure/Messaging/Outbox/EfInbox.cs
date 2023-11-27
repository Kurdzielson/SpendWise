using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Shared.Infrastructure.Messaging.Outbox;

internal sealed class EfInbox<T>(T dbContext, IClock clock, OutboxOptions outboxOptions, ILogger<EfInbox<T>> logger)
    : IInbox
    where T : DbContext
{
    private readonly DbSet<InboxMessage> _set = dbContext.Set<InboxMessage>();

    public bool Enabled { get; } = outboxOptions.Enabled;

    public async Task HandleAsync(Guid messageId, string name, Func<Task> handler)
    {
        var module = dbContext.GetModuleName();
        if (!Enabled)
        {
            logger.LogWarning($"Outbox is disabled ('{module}'), incoming messages won't be processed.");
            return;
        }

        logger.LogTrace($"Received a message with ID: '{messageId}' to be processed ('{module}').");
        if (await _set.AnyAsync(m => m.Id == messageId && m.ProcessedAt != null))
        {
            logger.LogTrace($"Message with ID: '{messageId}' was already processed ('{module}').");
            return;
        }

        logger.LogTrace($"Processing a message with ID: '{messageId}' ('{module}')...");

        var inboxMessage = new InboxMessage
        {
            Id = messageId,
            Name = name,
            ReceivedAt = clock.CurrentDate()
        };

        var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            await handler();
            inboxMessage.ProcessedAt = clock.CurrentDate();
            await _set.AddAsync(inboxMessage);
            await dbContext.SaveChangesAsync();

            if (transaction is not null)
            {
                await transaction.CommitAsync();
            }

            logger.LogTrace($"Processed a message with ID: '{messageId}' ('{module}').");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"There was an error when processing a message with ID: '{messageId}' ('{module}').");
            if (transaction is not null)
            {
                await transaction.RollbackAsync();
            }

            throw;
        }
        finally
        {
            {
                await transaction.DisposeAsync();
            }
        }
    }

    public async Task CleanupAsync(DateTime? to = null)
    {
        var module = dbContext.GetModuleName();
        if (!Enabled)
        {
            logger.LogWarning($"Outbox is disabled ('{module}'), incoming messages won't be cleaned up.");
            return;
        }

        var dateTo = to ?? clock.CurrentDate();
        var sentMessages = await _set.Where(x => x.ReceivedAt <= dateTo).ToListAsync();
        if (!sentMessages.Any())
        {
            logger.LogTrace($"No received messages found in inbox ('{module}') till: {dateTo}.");
            return;
        }

        logger.LogInformation(
            $"Found {sentMessages.Count} received messages in inbox ('{module}') till: {dateTo}, cleaning up...");
        _set.RemoveRange(sentMessages);
        await dbContext.SaveChangesAsync();
        logger.LogInformation(
            $"Removed {sentMessages.Count} received messages from inbox ('{module}') till: {dateTo}.");
    }
}