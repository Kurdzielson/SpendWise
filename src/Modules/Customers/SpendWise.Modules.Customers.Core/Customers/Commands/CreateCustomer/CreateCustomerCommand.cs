using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;

internal record CreateCustomerCommand(string Email) : ICommand<CreateResponse>;