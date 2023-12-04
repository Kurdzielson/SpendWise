using System.Text.RegularExpressions;
using SpendWise.Modules.Expenses.Core.Tags.ValueObjects.ColorHex.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects;

namespace SpendWise.Modules.Expenses.Core.Tags.ValueObjects.ColorHex;

internal class TagColorHex : ValueObject
{
    public string Value { get; set; }
    private static readonly Regex ColorHexRegex = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$", RegexOptions.Compiled);

    public TagColorHex(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !ColorHexRegex.IsMatch(value))
            throw new InvalidTagColorHexException(value);

        Value = value;
    }

    public static implicit operator TagColorHex(string value)
        => new(value);

    public static implicit operator string(TagColorHex value)
        => value.Value;
}