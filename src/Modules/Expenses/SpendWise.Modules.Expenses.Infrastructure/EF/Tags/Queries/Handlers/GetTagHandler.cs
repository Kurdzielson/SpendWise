using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Application.Tags.Queries.GetSingle;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Queries.Handlers;

internal class GetTagHandler(ExpensesReadDbContext context) : IQueryHandler<GetTagQuery, TagDto>
{
    public Task<TagDto> HandleAsync(GetTagQuery query, CancellationToken cancellationToken = default)
    {
        var tag = context.Tags.AsNoTracking()
            .Where(q => q.Id == query.TagId)
            .Select(q => q.AsDto())
            .FirstOrDefaultAsync(cancellationToken);

        return tag;
    }
}