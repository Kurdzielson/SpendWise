using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerAlreadyExistException(Guid customerId)
    : SpendWiseException($"Customer with Id: {customerId} already exist.")
{
    public Guid CustomerId { get; set; } = customerId;
}