using SpendWise.Modules.Expenses.Core.Expenses.Entities;

namespace SpendWise.Modules.Expenses.Core.Expenses.Repositories;

internal interface IExpenseRepository
{
    Task<Expense> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Expense expense, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Expense expense, CancellationToken cancellationToken);
    Task RemoveAsync(Expense expense, CancellationToken cancellationToken);
}