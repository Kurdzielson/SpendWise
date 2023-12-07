namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Delete;

internal record DeleteExpenseCommand(Guid ExpenseId) : ICommand;