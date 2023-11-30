using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Customers.Core.Customers.DAL;
using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Customers.Core.Customers.Queries.GetCustomer;

internal class GetCustomerHandler(CustomersReadDbContext context) : IQueryHandler<GetCustomerQuery, CustomerDetailsDto>
{
    public async Task<CustomerDetailsDto> HandleAsync(GetCustomerQuery query,
        CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers
            .AsNoTracking()
            .Where(q => q.Id == query.CustomerId)
            .Select(q => q.AsDetailsDto())
            .FirstOrDefaultAsync(cancellationToken);

        return customer;
    }
}