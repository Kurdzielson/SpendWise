using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Application.Tags.Queries.GetPaginated;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read.Model;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Queries;
using SpendWise.Shared.Infrastructure.Postgres;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Queries.Handlers;

internal class GetTagsHandler(ExpensesReadDbContext expensesContext, IContext context)
    : IQueryHandler<GetTagsQuery, Paged<TagDto>>
{
    public async Task<Paged<TagDto>> HandleAsync(GetTagsQuery query, CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;

        var tags = expensesContext.Tags.AsNoTracking();
        tags = Filter(tags, query);

        var result = await tags
            .Where(q => q.CustomerId == customerId)
            .Select(q => q.AsDto())
            .PaginateAsync(query, cancellationToken);

        return result;
    }

    private static IQueryable<TagReadModel> Filter(IQueryable<TagReadModel> tags, GetTagsQuery query)
    {
        if (!string.IsNullOrWhiteSpace(query.Name))
            tags = tags.Where(q => Microsoft.EntityFrameworkCore.EF.Functions.ILike(q.Name, query.Name));

        return tags;
    }
}