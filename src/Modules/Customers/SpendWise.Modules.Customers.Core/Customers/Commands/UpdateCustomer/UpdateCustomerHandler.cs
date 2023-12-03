using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;

internal class UpdateCustomerHandler
(ICustomerRepository customerRepository,
    ILogger<UpdateCustomerHandler> logger) : ICommandHandler<UpdateCustomerCommand, UpdateResponse>
{
    public async Task<UpdateResponse> HandleAsync(UpdateCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(command.CustomerId, cancellationToken)
                       ?? throw new CustomerNotFoundException(command.CustomerId);

        if (customer.State != AvailableCustomerStates.Completed && customer.State != AvailableCustomerStates.Verified)
            throw new CustomerIsNotCompletedException(customer.Id);

        if (await customerRepository.DoesExistAsync(command.Nick, cancellationToken))
            throw new NickIsAlreadyInUseException(command.Nick);

        customer.Update(command.Nick, command.FullName);

        var customerId = await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customerId}' has been updated.");

        return new UpdateResponse(customerId);
    }
}