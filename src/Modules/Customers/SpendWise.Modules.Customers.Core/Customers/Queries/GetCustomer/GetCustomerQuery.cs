using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Customers.Core.Customers.Queries.GetCustomer;

internal record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerDetailsDto>;