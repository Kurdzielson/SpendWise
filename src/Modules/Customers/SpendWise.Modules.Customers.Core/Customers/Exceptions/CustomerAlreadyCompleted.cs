using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerAlreadyCompleted(Guid customerId)
    : SpendWiseException($"Customer with Id: '{customerId}' is already completed")
{
    public Guid CustomerId { get; } = customerId;
}