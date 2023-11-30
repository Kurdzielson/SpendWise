using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;

internal record CreateCustomerCommand(string Email) : ICommand;