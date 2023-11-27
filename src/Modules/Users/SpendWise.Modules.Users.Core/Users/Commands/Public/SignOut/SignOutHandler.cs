using Microsoft.Extensions.Logging;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignOut;

internal class SignOutHandler(ILogger<SignOutHandler> logger) : ICommandHandler<SignOutCommand>
{
    public async Task HandleAsync(SignOutCommand command, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        logger.LogInformation($"User with ID: '{command.UserId}' has signed out.");
    }
}