using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Modules.Users.Core.Users.Events;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;

internal class UnlockUserHandler(IUserRepository userRepository, ILogger<UnlockUserHandler> logger,
    IMessageBroker messageBroker) : ICommandHandler<UnlockUserCommand>
{
    public async Task HandleAsync(UnlockUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
                   ?? throw new UserNotFoundException(command.UserId);

        if (user.State == AvailableUserStates.Active)
            throw new UserIsActive(user.Id);

        user.Unlock();

        var userId = await userRepository.UpdateAsync(user, cancellationToken);
        logger.LogInformation($"User with Id: '{userId}' has been unlocked.");
        await messageBroker.PublishAsync(new Unlocked(user.Id), cancellationToken);
    }
}