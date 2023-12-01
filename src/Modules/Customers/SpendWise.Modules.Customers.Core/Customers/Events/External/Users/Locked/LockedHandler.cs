using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Locked;

internal class LockedHandler(ICustomerRepository customerRepository,
    ILogger<LockedHandler> logger) : IEventHandler<Locked>
{
    public async Task HandleAsync(Locked @event, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(@event.UserId, cancellationToken)
                       ?? throw new CustomerNotFoundException(@event.UserId);

        customer.Lock();

        await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customer.Id}' ahs been locked.");
    }
}