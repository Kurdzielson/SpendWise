using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Amount.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Amount;

internal class ExpenseAmount : ValueObject
{
    public decimal Value { get; set; }

    private const decimal MinAmount = 0;

    public ExpenseAmount(decimal value)
    {
        if (value < MinAmount)
            throw new InvalidExpenseAmountException(value);

        Value = value;
    }

    public static implicit operator ExpenseAmount(decimal value)
        => new(value);

    public static implicit operator decimal(ExpenseAmount value)
        => value.Value;
}