using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description;

internal class ExpenseDescription : ValueObject
{
    public string Value { get; set; }

    private const int MaxLength = 500;


    public ExpenseDescription(string value)
    {
        if (!string.IsNullOrWhiteSpace(value) && value.Length > MaxLength)
            throw new InvalidExpenseDescriptionException(value);

        Value = value;
    }

    public static implicit operator ExpenseDescription(string value)
        => new(value);

    public static implicit operator string(ExpenseDescription value)
        => value.Value;
}