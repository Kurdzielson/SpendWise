using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Expenses.Repositories;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Repositories;

internal class ExpenseRepository(ExpensesWriteDbContext context) : IExpenseRepository
{
    private readonly DbSet<Expense> _expenses = context.Expenses;

    public async Task<Expense> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _expenses.AsNoTracking().Where(q => q.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<Guid> AddAsync(Expense expense, CancellationToken cancellationToken)
    {
        var result = await _expenses.AddAsync(expense, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(Expense expense, CancellationToken cancellationToken)
    {
        var result = _expenses.Update(expense);
        await context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task RemoveAsync(Expense expense, CancellationToken cancellationToken)
    {
        _expenses.Remove(expense);
        await context.SaveChangesAsync(cancellationToken);
    }
}