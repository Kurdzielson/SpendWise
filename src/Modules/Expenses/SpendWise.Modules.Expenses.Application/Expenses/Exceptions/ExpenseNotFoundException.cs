namespace SpendWise.Modules.Expenses.Application.Expenses.Exceptions;

internal class ExpenseNotFoundException(Guid expenseId)
    : SpendWiseException($"Expense with Id: '{expenseId}' not found.")
{
    public Guid ExpenseId { get; } = expenseId;
}