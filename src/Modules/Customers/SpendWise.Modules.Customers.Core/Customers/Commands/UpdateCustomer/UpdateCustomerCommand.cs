using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UpdateCustomer;

internal record UpdateCustomerCommand(string Nick, string FullName) : ICommand
{
    internal Guid CustomerId { get; init; }
}