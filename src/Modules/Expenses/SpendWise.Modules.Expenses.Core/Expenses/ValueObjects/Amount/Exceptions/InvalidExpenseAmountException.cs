using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Amount.Exceptions;

internal class InvalidExpenseAmountException(decimal amount) : SpendWiseException($"Expense amount: '{amount}' is invalid.")
{
    public decimal Amount { get; } = amount;
}