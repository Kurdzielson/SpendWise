using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category.Exceptions;

internal class UnsupportedExpenseCategoryCodeException(string code) : SpendWiseException($"Unsupported Expense category code: '{code}'.")
{
    public string Code { get; } = code;
}