using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Outbox;

public interface IOutbox
{
    bool Enabled { get; }
    Task SaveAsync(params IMessage[] messages);
    Task PublishUnsentAsync();
    Task CleanupAsync(DateTime? to = null);
}