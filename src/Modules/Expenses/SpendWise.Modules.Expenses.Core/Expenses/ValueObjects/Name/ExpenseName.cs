using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Name.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Name;

internal class ExpenseName : ValueObject
{
    public string Value { get; set; }
    private const int MaxLength = 50;

    public ExpenseName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidExpenseNameException(value);

        if (value.Trim().Length > MaxLength)
            throw new InvalidExpenseNameException(value);

        Value = value;
    }

    public static implicit operator ExpenseName(string value)
        => new(value);

    public static implicit operator string(ExpenseName value)
        => value.Value;
}