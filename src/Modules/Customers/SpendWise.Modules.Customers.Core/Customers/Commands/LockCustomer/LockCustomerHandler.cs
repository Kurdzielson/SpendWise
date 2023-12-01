using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Modules.Customers.Core.Customers.Events;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;

internal class LockCustomerHandler(ICustomerRepository customerRepository, ILogger<LockCustomerHandler> logger,
    IMessageBroker messageBroker) : ICommandHandler<LockCustomerCommand>
{
    public async Task HandleAsync(LockCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(command.CustomerId, cancellationToken)
                       ?? throw new CustomerNotFoundException(command.CustomerId);

        if (customer.State == AvailableCustomerStates.Locked)
            throw new CustomerLockedException(customer.Id);

        customer.Lock();

        await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customer.Id}' has been locked.");
        await messageBroker.PublishAsync(new Locked(customer.Id), cancellationToken);
    }
}