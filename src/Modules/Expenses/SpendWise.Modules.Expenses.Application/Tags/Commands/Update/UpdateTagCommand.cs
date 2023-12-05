namespace SpendWise.Modules.Expenses.Application.Tags.Commands.Update;

internal record UpdateTagCommand(string Name, string ColorHex) : ICommand<UpdateResponse>
{
    internal Guid TagId { get; set; }
}