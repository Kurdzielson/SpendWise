using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Delete;

internal record DeleteTagCommand(Guid TagId) : ICommand;