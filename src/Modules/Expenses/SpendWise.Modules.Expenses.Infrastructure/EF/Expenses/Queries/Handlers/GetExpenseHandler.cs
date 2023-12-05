using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Application.Expenses.DTO;
using SpendWise.Modules.Expenses.Application.Expenses.Queries.GetSingle;
using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Queries;
using SpendWise.Shared.Abstraction.Contexts;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Queries.Handlers;

internal class GetExpenseHandler(ExpensesReadDbContext expensesContext, IContext context)
    : IQueryHandler<GetExpenseQuery, ExpenseDetailsDto>
{
    public async Task<ExpenseDetailsDto> HandleAsync(GetExpenseQuery query,
        CancellationToken cancellationToken = default)
    {
        var customerId = context.Identity.Id;
        var expense = await expensesContext.Expenses.AsNoTracking()
            .Where(q => q.Id == query.ExpenseId && q.CustomerId == customerId)
            .FirstOrDefaultAsync(cancellationToken);

        var tags = new List<TagDto>();
        
        foreach (var tagId in expense.TagIds)
        {
            var tag = await expensesContext.Tags
                .Where(q => q.Id == tagId)
                .Select(q=>q.AsDto())
                .FirstOrDefaultAsync(cancellationToken);
            
            tags.Add(tag);
        }

        var result = expense.AsDetailsDto(tags);

        return result;
    }
}