using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Outbox;

public interface IOutboxBroker
{
    bool Enabled { get; }
    Task SendAsync(params IMessage[] messages);
}