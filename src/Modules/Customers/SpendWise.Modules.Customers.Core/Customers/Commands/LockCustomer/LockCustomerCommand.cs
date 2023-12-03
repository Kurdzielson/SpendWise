using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.LockCustomer;

internal record LockCustomerCommand(Guid CustomerId) : ICommand<UpdateResponse>;