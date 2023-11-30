using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick;

public class Nick : ValueObject
{
    public string? Value { get; }
    private const int MaxLength = 30;
    private const int MinLength = 6;

    public Nick(string? value)
    {
        if (value is null)
        {
            Value = value;
            return;
        }

        if (value.Length is > MaxLength or < MinLength)
        {
            throw new InvalidNickException(value);
        }
        
        Value = value;
    }

    public static implicit operator Nick(string? value) => new(value);
    public static implicit operator string?(Nick value) => value.Value ?? null;
}