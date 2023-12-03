using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.UnlockCustomer;

internal record UnlockCustomerCommand(Guid CustomerId) : ICommand<UpdateResponse>;