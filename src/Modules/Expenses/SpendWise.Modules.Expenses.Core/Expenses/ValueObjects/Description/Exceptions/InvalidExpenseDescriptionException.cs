using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description.Exceptions;

internal class InvalidExpenseDescriptionException(string description) : SpendWiseException($"Expense description: '{description}' is invalid.")
{
    public string Description { get; } = description;
}