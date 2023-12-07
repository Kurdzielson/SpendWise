using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Application.Expenses.DTO;
using SpendWise.Modules.Expenses.Application.Expenses.Queries.GetPaginated;
using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Read.Model;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Queries;
using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Queries.Handlers;

internal class GetPaginatedExpensesHandler(ExpensesReadDbContext expensesContext, IContext context)
    : IQueryHandler<GetPaginatedExpensesQuery, Paged<ExpenseDto>>
{
    public async Task<Paged<ExpenseDto>> HandleAsync(GetPaginatedExpensesQuery query,
        CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;
        var expenses = expensesContext.Expenses.AsNoTracking();
        expenses = Filter(expenses, query);

        var result = await expenses.Where(q => q.CustomerId == customerId)
            .Select(q => q.AsDto())
            .PaginateAsync(query, cancellationToken);

        return result;
    }

    private IQueryable<ExpenseReadModel> Filter(IQueryable<ExpenseReadModel> expenses, GetPaginatedExpensesQuery query)
    {
        expenses = !string.IsNullOrWhiteSpace(query.SortOrder) &&
                   query.SortOrder.Equals("asc", StringComparison.CurrentCultureIgnoreCase)
            ? expenses.OrderBy(q => q.Date)
            : expenses.OrderByDescending(q => q.Date);

        return expenses;
    }
}