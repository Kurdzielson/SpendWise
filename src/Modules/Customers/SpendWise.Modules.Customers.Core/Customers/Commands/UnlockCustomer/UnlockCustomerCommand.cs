using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;

internal record UnlockCustomerCommand(Guid CustomerId) : ICommand;