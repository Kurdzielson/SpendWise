using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Create;

internal record CreateTagCommand(string Name, string ColorHex) : ICommand<CreateResponse>;