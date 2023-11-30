using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;

internal record LockCustomerCommand(Guid CustomerId) : ICommand;