using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;

internal record UpdateCustomerCommand(string Nick, string FullName) : ICommand<UpdateResponse>
{
    internal Guid CustomerId { get; init; }
}