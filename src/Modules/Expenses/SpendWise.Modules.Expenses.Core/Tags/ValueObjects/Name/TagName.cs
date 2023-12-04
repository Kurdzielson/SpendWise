using SpendWise.Modules.Expenses.Core.Tags.ValueObjects.Name.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects;

namespace SpendWise.Modules.Expenses.Core.Tags.ValueObjects.Name;

internal class TagName : ValueObject
{
    public string Value { get; set; }

    private const int MaxLength = 50;

    public TagName(string value)
    {
        if (value is not null && value.Length > MaxLength)
            throw new InvalidTagNameException(value);

        Value = value?.Trim();
    }

    public static implicit operator TagName(string value)
        => value is null ? null : new TagName(value);

    public static implicit operator string(TagName value)
        => value.Value;
}