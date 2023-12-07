using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Name.Exceptions;

internal class InvalidExpenseNameException(string name) : SpendWiseException($"Expense Name: '{name}' is invalid.")
{
    public string Name { get; } = name;
}