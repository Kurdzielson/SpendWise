using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpendWise.Shared.Abstraction.Messaging;
using SpendWise.Shared.Abstraction.Modules;
using SpendWise.Shared.Abstraction.Time;
using SpendWise.Shared.Infrastructure.Contexts;
using SpendWise.Shared.Infrastructure.Messaging.Contexts;
using SpendWise.Shared.Infrastructure.Messaging.Dispatchers;
using SpendWise.Shared.Infrastructure.Serialization;

namespace SpendWise.Shared.Infrastructure.Messaging.Outbox;

internal sealed class EfOutbox<T>(T dbContext, IMessageContextRegistry messageContextRegistry,
        IMessageContextProvider messageContextProvider, IClock clock, IModuleClient moduleClient,
        IAsyncMessageDispatcher asyncMessageDispatcher, IJsonSerializer jsonSerializer,
        MessagingOptions messagingOptions, OutboxOptions outboxOptions, ILogger<EfOutbox<T>> logger)
    : IOutbox
    where T : DbContext
{
    private readonly DbSet<OutboxMessage> _set = dbContext.Set<OutboxMessage>();

    public bool Enabled { get; } = outboxOptions.Enabled;

    public async Task SaveAsync(params IMessage[] messages)
    {
        var module = dbContext.GetModuleName();
        if (!Enabled)
        {
            logger.LogWarning($"Outbox is disabled ('{module}'), outgoing messages won't be saved.");
            return;
        }

        if (messages is null || !messages.Any())
        {
            logger.LogWarning($"No messages have been provided to be saved to the outbox ('{module}').");
            return;
        }

        var outboxMessages = messages.Where(x => x is not null)
            .Select(x =>
            {
                var context = messageContextProvider.Get(x);
                return new OutboxMessage
                {
                    Id = context.MessageId,
                    CorrelationId = context.Context.CorrelationId,
                    Name = x.GetType().Name.Underscore(),
                    Data = jsonSerializer.Serialize((object)x),
                    Type = x.GetType().AssemblyQualifiedName,
                    CreatedAt = clock.CurrentDate(),
                    TraceId = context.Context.TraceId,
                    UserId = context.Context.Identity.Id
                };
            }).ToArray();

        if (!outboxMessages.Any())
        {
            logger.LogWarning($"No messages have been provided to be saved to the outbox ('{module}').");
            return;
        }

        await _set.AddRangeAsync(outboxMessages);
        await dbContext.SaveChangesAsync();
        logger.LogInformation($"Saved {outboxMessages.Length} messages to the outbox ('{module}').");
    }

    public async Task PublishUnsentAsync()
    {
        var module = dbContext.GetModuleName();
        if (!Enabled)
        {
            logger.LogWarning($"Outbox is disabled ('{module}'), outgoing messages won't be sent.");
            return;
        }

        var unsentMessages = await _set.Where(x => x.SentAt == null).ToListAsync();
        if (!unsentMessages.Any())
        {
            logger.LogTrace($"No unsent messages found in outbox ('{module}').");
            return;
        }

        logger.LogTrace($"Found {unsentMessages.Count} unsent messages in outbox ('{module}'), sending...");
        foreach (var outboxMessage in unsentMessages)
        {
            var type = Type.GetType(outboxMessage.Type);
            var message = jsonSerializer.Deserialize(outboxMessage.Data, type) as IMessage;
            if (message is null)
            {
                logger.LogError(
                    $"Invalid message type in outbox ('{module}'): '{type.Name}', name: '{outboxMessage.Name}', " +
                    $"ID: '{outboxMessage.Id}' ('{module}').");
                continue;
            }

            var messageId = outboxMessage.Id;
            var correlationId = outboxMessage.CorrelationId;
            var sentAt = clock.CurrentDate();
            var name = message.GetType().Name.Underscore();
            messageContextRegistry.Set(message, new MessageContext(messageId, new Context(correlationId,
                outboxMessage.TraceId,
                new IdentityContext(outboxMessage.UserId))));

            logger.LogInformation(
                "Publishing a message from outbox ('{Module}'): {Name} [Message ID: {MessageId}, Correlation ID: {CorrelationId}]...",
                module, name, messageId, correlationId);

            if (messagingOptions.UseAsyncDispatcher)
            {
                await asyncMessageDispatcher.PublishAsync(message);
            }
            else
            {
                await moduleClient.PublishAsync(message);
            }

            outboxMessage.SentAt = sentAt;
            _set.Update(outboxMessage);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task CleanupAsync(DateTime? to = null)
    {
        var module = dbContext.GetModuleName();
        if (!Enabled)
        {
            logger.LogWarning($"Outbox is disabled ('{module}'), outgoing messages won't be cleaned up.");
            return;
        }

        var dateTo = to ?? clock.CurrentDate();
        var sentMessages = await _set.Where(x => x.SentAt != null && x.CreatedAt <= dateTo).ToListAsync();
        if (!sentMessages.Any())
        {
            logger.LogTrace($"No sent messages found in outbox ('{module}') till: {dateTo}.");
            return;
        }

        logger.LogTrace(
            $"Found {sentMessages.Count} sent messages in outbox ('{module}') till: {dateTo}, cleaning up...");
        _set.RemoveRange(sentMessages);
        await dbContext.SaveChangesAsync();
        logger.LogTrace($"Removed {sentMessages.Count} sent messages from outbox ('{module}') till: {dateTo}.");
    }
}