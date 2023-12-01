using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events.External.Customers.Unlocked;

internal class UnlockedHandler(IUserRepository userRepository, ILogger<UnlockedHandler> logger)
    : IEventHandler<Unlocked>
{
    public async Task HandleAsync(Unlocked @event, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(@event.CustomerId, cancellationToken)
                   ?? throw new UserNotFoundException(@event.CustomerId);

        user.Unlock();

        await userRepository.UpdateAsync(user, cancellationToken);
        logger.LogInformation($"User with Id: '{user.Id}' has been unlocked");
    }
}