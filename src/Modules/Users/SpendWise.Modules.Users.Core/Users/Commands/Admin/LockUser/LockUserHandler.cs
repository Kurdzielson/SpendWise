using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Modules.Users.Core.Users.Events;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Messaging;
using SpendWise.Shared.Infrastructure.Api;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.LockUser;

internal class LockUserHandler(IUserRepository userRepository, ILogger<LockUserHandler> logger,
    IMessageBroker messageBroker) : ICommandHandler<LockUserCommand>
{
    public async Task HandleAsync(LockUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
                   ?? throw new UserNotFoundException(command.UserId);

        if (user.Role.Name.Equals(Role.Admin, StringComparison.InvariantCultureIgnoreCase))
            throw new CannotLockAdminAccountException();

        if (user.State == AvailableUserStates.Locked)
            throw new UserIsLocked(user.Id);

        user.Lock();

        var userId = await userRepository.UpdateAsync(user, cancellationToken);
        logger.LogInformation($"User with Id: '{userId}' has been locked.");
        await messageBroker.PublishAsync(new Locked(user.Id), cancellationToken);
    }
}