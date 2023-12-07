namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Create;

internal record CreateExpenseCommand(DateTimeOffset Date, string Name, decimal Amount, string Description,
    string Category, string Currency) : ICommand<CreateResponse>;