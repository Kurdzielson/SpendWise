using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerNotFoundException : SpendWiseException
{
    public Guid CustomerId { get; set; }

    public CustomerNotFoundException(Guid customerId) : base($"Customer with Id: {customerId} not found.")
    {
        CustomerId = customerId;
    }
}