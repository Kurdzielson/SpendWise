using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerLockedException(Guid customerId)
    : SpendWiseException($"Customer with Id: '{customerId} is locked.")
{
    public Guid CustomerId = customerId;
}