using SpendWise.Modules.Expenses.Application.Expenses.Commands.Update;

namespace SpendWise.Modules.Expenses.API.Expenses.Endpoints.User.UpdateExpense;

internal class UpdateExpenseRequest
{
    [FromRoute(Name = "expenseId")] public Guid ExpenseId { get; init; }
    [FromBody] public UpdateExpenseCommand Command { get; init; }
}