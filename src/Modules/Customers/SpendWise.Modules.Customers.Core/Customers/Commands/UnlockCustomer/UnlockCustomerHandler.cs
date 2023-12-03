using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Modules.Customers.Core.Customers.Events;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;

internal class UnlockCustomerHandler(ICustomerRepository customerRepository, ILogger<UnlockCustomerHandler> logger,
    IMessageBroker messageBroker) : ICommandHandler<UnlockCustomerCommand, UpdateResponse>
{
    public async Task<UpdateResponse> HandleAsync(UnlockCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(command.CustomerId, cancellationToken)
                       ?? throw new CustomerNotFoundException(command.CustomerId);

        if (customer.State != AvailableCustomerStates.Locked)
            throw new CustomerIsNotLockedException(customer.Id);

        customer.Unlock();

        var customerId = await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customerId}' has been unlocked.");
        await messageBroker.PublishAsync(new Unlocked(customerId), cancellationToken);

        return new UpdateResponse(customerId);
    }
}