using SpendWise.Modules.Expenses.Application.Expenses.DTO;

namespace SpendWise.Modules.Expenses.Application.Expenses.Queries.GetSingle;

internal record GetExpenseQuery(Guid ExpenseId) : IQuery<ExpenseDetailsDto>;