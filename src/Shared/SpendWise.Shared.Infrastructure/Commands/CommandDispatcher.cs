using Microsoft.Extensions.DependencyInjection;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Shared.Infrastructure.Commands;

internal sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        if (command is null)
        {
            return;
        }

        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command, cancellationToken);
    }
}