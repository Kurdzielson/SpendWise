using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;
using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;

internal class VerifyCustomerHandler(ICustomerRepository customerRepository, ILogger<VerifyCustomerHandler> logger,
    IClock clock) : ICommandHandler<VerifyCustomerCommand, UpdateResponse>
{
    public async Task<UpdateResponse> HandleAsync(VerifyCustomerCommand command,
        CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetAsync(command.CustomerId, cancellationToken)
                       ?? throw new CustomerNotFoundException(command.CustomerId);

        if (customer.CompletedAt is null)
            throw new CustomerIsNotCompletedException(customer.Id);

        if (customer.VerifiedAt is not null)
            throw new CustomerIsVerifiedException(customer.Id);

        var now = clock.CurrentDateTimeOffset();
        customer.Verify(now);

        var customerId = await customerRepository.UpdateAsync(customer, cancellationToken);
        logger.LogInformation($"Customer with Id: '{customerId}' has been verified.");

        return new UpdateResponse(customerId);
    }
}