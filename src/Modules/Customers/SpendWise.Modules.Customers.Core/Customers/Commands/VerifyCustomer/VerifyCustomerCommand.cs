using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.VerifyCustomer;

internal record VerifyCustomerCommand(Guid CustomerId) : ICommand<UpdateResponse>;