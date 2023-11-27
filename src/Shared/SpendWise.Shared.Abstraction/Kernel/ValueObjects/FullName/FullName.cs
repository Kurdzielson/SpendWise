using SpendWise.Shared.Abstraction.Kernel.ValueObjects.FullName.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.FullName;

public class FullName : ValueObject
{
    public string? Value { get; }
    private const int MaxLength = 100;

    public FullName(string? value)
    {
        if (value is null)
        {
            Value = value;
            return;
        }

        if (string.IsNullOrWhiteSpace(value) || value.Length is > MaxLength)
        {
            throw new InvalidFullNameException(value);
        }

        Value = value;
    }

    public static implicit operator FullName(string? value) => new(value);
    public static implicit operator string?(FullName value) => value.Value ?? null;
}