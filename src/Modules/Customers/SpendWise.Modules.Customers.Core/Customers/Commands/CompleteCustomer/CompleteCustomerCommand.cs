using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CompleteCustomer;

internal record CompleteCustomerCommand(string Nick, string FullName) : ICommand<UpdateResponse>
{
    internal Guid CustomerId { get; init; }
}