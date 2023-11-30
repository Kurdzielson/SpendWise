using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CompleteCustomer;

internal record CompleteCustomerCommand(string Nick, string FullName) : ICommand
{
    internal Guid CustomerId { get; init; }
}