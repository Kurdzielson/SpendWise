using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Tags.ValueObjects.Name.Exceptions;

internal class InvalidTagNameException(string tagName) : SpendWiseException($"Tag name: '{tagName}' is invalid.")
{
    public string TagName { get; } = tagName;
}