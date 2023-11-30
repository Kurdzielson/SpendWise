using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;

internal record VerifyCustomerCommand(Guid CustomerId) : ICommand;