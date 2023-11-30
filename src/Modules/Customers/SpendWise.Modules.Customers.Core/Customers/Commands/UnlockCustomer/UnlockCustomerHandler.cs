using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;

internal class UnlockCustomerHandler(ICustomerRepository customerRepository,
    ILogger<UnlockCustomerHandler> logger) : ICommandHandler<UnlockCustomerCommand>
{
    public async Task HandleAsync(UnlockCustomerCommand command, CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(command.CustomerId, cancellationToken)
                       ?? throw new CustomerNotFoundException(command.CustomerId);

        if (customer.State != AvailableCustomerStates.Locked)
            throw new CustomerIsNotLockedException(customer.Id);

        customer.Unlock();

        await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customer.Id}' has been unlocked.");
    }
}