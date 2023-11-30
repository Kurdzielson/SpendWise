using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Customers.Core.Customers.Exceptions;

internal class CustomerIsCompletedException(Guid customerId) : SpendWiseException($"Customer with Id: '{customerId}' has been already completed.")
{
    
}