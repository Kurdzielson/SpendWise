using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Tags.ValueObjects.ColorHex.Exceptions;

internal class InvalidTagColorHexException(string colorHex)
    : SpendWiseException($"Tag color HEX '{colorHex}' is invalid.")
{
    public string ColorHex { get; } = colorHex;
}