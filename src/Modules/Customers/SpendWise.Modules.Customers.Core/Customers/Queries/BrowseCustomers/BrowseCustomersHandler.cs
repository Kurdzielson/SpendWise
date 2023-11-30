using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Customers.Core.Customers.DAL;
using SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read.Model;
using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Shared.Abstraction.Queries;
using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Customers.Core.Customers.Queries.BrowseCustomers;

internal class BrowseCustomersHandler
    (CustomersReadDbContext context) : IQueryHandler<BrowseCustomersQuery, Paged<CustomerDto>>
{
    public async Task<Paged<CustomerDto>> HandleAsync(BrowseCustomersQuery query,
        CancellationToken cancellationToken = default)
    {
        var customers = context.Customers.AsNoTracking();

        customers = Filter(query, customers);

        var result = await customers
            .Select(q => q.AsDto())
            .PaginateAsync(query, cancellationToken);

        return result;
    }


    private static IQueryable<CustomerReadModel> Filter(BrowseCustomersQuery query,
        IQueryable<CustomerReadModel> customers)
    {
        if (!string.IsNullOrEmpty(query.State))
            customers = customers.Where(q => EF.Functions.ILike(q.State, query.State));

        if (!string.IsNullOrEmpty(query.Email))
            customers = customers.Where(q => EF.Functions.ILike(q.Email, $"%{query.Email}%"));

        if (!string.IsNullOrEmpty(query.Nick))
            customers = customers.Where(q => EF.Functions.ILike(q.Nick, $"%{query.Nick}%"));

        if (query.CreatedAtFrom is not null)
            customers = customers.Where(q => q.CreatedAt >= query.CreatedAtFrom);

        if (query.CreatedAtTo is not null)
            customers = customers.Where(q => q.CreatedAt <= query.CreatedAtTo);

        if (query.CompletedAtFrom is not null)
            customers = customers.Where(q => q.CompletedAt >= query.CompletedAtFrom);

        if (query.CompletedAtTo is not null)
            customers = customers.Where(q => q.CompletedAt <= query.CompletedAtTo);

        if (query.VerifiedAtFrom is not null)
            customers = customers.Where(q => q.VerifiedAt >= query.VerifiedAtFrom);

        if (query.VerifiedAtTo is not null)
            customers = customers.Where(q => q.VerifiedAt <= query.VerifiedAtTo);

        customers = !string.IsNullOrWhiteSpace(query.SortOrder) &&
                    query.SortOrder.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
            ? customers.OrderBy(q => q.CreatedAt)
            : customers.OrderByDescending(q => q.CreatedAt);

        return customers;
    }
}