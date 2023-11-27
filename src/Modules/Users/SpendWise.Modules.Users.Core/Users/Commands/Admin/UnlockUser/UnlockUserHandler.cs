using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;

internal class UnlockUserHandler(IUserRepository userRepository, ILogger<UnlockUserHandler> logger) : ICommandHandler<UnlockUserCommand>
{
    public async Task HandleAsync(UnlockUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
                   ?? throw new UserNotFoundException(command.UserId);

        if (user.Role.Name.Equals(Role.Admin, StringComparison.InvariantCultureIgnoreCase))
            throw new CannotLockAdminAccountException();

        if (user.State == AvailableUserStates.Active)
            throw new UserIsLocked(user.Id);
        
        user.Lock();

        var userId = await userRepository.UpdateAsync(user, cancellationToken);
        logger.LogInformation($"User with Id: '{userId}' has been locked.");
    }
}