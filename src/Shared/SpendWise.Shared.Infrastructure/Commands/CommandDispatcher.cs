using Microsoft.Extensions.DependencyInjection;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Shared.Infrastructure.Commands;

internal sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        if (command is null)
            return;


        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command, cancellationToken);
    }

    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : class, ICommand<TResult> where TResult : class
    {
        if (command is null)
            return null;

        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

        var result = await handler.HandleAsync(command, cancellationToken);
        return result;
    }
}