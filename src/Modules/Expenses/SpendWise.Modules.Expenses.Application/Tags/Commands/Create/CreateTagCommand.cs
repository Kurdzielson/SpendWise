namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Create;

internal record CreateTagCommand(string Name, string ColorHex) : ICommand<CreateResponse>;