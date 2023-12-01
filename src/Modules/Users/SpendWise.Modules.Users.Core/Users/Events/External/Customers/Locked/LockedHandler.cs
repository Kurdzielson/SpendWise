using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events.External.Customers.Locked;

internal class LockedHandler(IUserRepository userRepository, ILogger<LockedHandler> logger) : IEventHandler<Locked>
{
    public async Task HandleAsync(Locked @event, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(@event.CustomerId, cancellationToken)
                   ?? throw new UserNotFoundException(@event.CustomerId);

        user.Lock();

        await userRepository.UpdateAsync(user, cancellationToken);
        logger.LogInformation($"User with Id: '{user.Id}' has been locked.");
    }
}