using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CompleteCustomer;

internal class CompleteCustomerHandler(ICustomerRepository customerRepository, ILogger<CompleteCustomerHandler> logger,
    IClock clock) : ICommandHandler<CompleteCustomerCommand, UpdateResponse>
{
    public async Task<UpdateResponse> HandleAsync(CompleteCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(command.CustomerId, cancellationToken)
                       ?? throw new CustomerNotFoundException(command.CustomerId);

        if (customer.CompletedAt is not null)
            throw new CustomerIsCompletedException(customer.Id);

        var now = clock.CurrentDateTimeOffset();
        customer.Complete(command.Nick, command.FullName, now);

        var customerId = await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customerId}' has been completed.");

        return new UpdateResponse(customerId);
    }
}