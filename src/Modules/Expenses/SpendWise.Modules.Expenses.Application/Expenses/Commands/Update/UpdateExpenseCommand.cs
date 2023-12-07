namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Update;

internal record UpdateExpenseCommand(DateTimeOffset Date, string Name, decimal Amount, string Currency,
    string Description, string Category) : ICommand<UpdateResponse>
{
    internal Guid ExpenseId { get; init; }
}