using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Unlocked;

internal class UnlockedHandler(ICustomerRepository customerRepository,
    ILogger<UnlockedHandler> logger) : IEventHandler<Unlocked>
{
    public async Task HandleAsync(Unlocked @event, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(@event.UserId, cancellationToken)
                       ?? throw new CustomerNotFoundException(@event.UserId);

        customer.Unlock();

        await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customer.Id}' has been unlocked.");
    }
}