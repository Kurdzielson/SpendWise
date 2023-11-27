using Microsoft.Extensions.DependencyInjection;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Outbox;

internal sealed class OutboxBroker(IServiceProvider serviceProvider, OutboxTypeRegistry registry, OutboxOptions options)
    : IOutboxBroker
{
    public bool Enabled { get; } = options.Enabled;

    public async Task SendAsync(params IMessage[] messages)
    {
        var message = messages[0]; // Not possible to send messages from different modules at once
        var outboxType = registry.Resolve(message);
        if (outboxType is null)
        {
            throw new InvalidOperationException($"Outbox is not registered for module: '{message.GetModuleName()}'.");
        }

        using var scope = serviceProvider.CreateScope();
        var outbox = (IOutbox)scope.ServiceProvider.GetRequiredService(outboxType);
        await outbox.SaveAsync(messages);
    }
}