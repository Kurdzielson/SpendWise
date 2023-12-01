using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Entities;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Shared.Abstraction.Events;
using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Created;

internal class CreatedHandler(IClock clock,
    ILogger<CreatedHandler> logger,
    ICustomerRepository customerRepository) : IEventHandler<Created>
{
    private const string ValidRole = "user";

    public async Task HandleAsync(Created @event, CancellationToken cancellationToken = default)
    {
        if (@event.Role is not ValidRole)
            return;

        var now = clock.CurrentDate();
        var customer = Customer.CreateFromUser(@event.UserId, @event.Email, now);

        await customerRepository.AddAsync(customer, cancellationToken);
        logger.LogInformation($"Created Customer based on User with Id: '{@event.UserId}'.");
    }
}