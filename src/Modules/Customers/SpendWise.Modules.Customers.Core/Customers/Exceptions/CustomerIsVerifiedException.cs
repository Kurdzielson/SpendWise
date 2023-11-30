using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerIsVerifiedException(Guid customerId) : SpendWiseException($"Customer with Id: '{customerId}' is already verified.")
{
    public Guid CustomerId { get; } = customerId;
}