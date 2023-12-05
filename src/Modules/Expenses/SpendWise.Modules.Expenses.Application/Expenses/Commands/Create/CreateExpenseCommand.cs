namespace SpendWise.Modules.Expenses.Application.Expenses.Commands.Create;

internal record CreateExpenseCommand(DateTimeOffset Date, decimal Amount, string Description, string Category,
    string Currency, IEnumerable<Guid> TagIds) : ICommand<CreateResponse>;