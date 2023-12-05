namespace SpendWise.Modules.Expenses.Application.Tags.Exceptions;

internal class TagNotFound(Guid tagId) : SpendWiseException($"Tag with Id: '{tagId}' not found.")
{
    public Guid TagId { get; } = tagId;
}