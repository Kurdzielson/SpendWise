using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerIsNotCompletedException(Guid customerId) : SpendWiseException($"Customer with Id: '{customerId}' isn't completed.")
{
    public Guid CustomerId { get; } = customerId;
}