using SpendWise.Modules.Customers.Core.Customers.Domain.Types;
using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerIsNotLockedException(Guid customerId) : SpendWiseException(
    $"Customer with Id: '{customerId} isn't locked'")
{
    public Guid CustomerId { get; } = customerId;
}