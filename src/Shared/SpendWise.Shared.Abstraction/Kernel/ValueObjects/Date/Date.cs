namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;

public class Date(DateTimeOffset? value) : ValueObject
{
    public DateTimeOffset? Value { get; } = value;

    public static implicit operator Date?(DateTimeOffset? value) => value is null ? null : new Date(value);
    public static implicit operator DateTimeOffset?(Date value) => value?.Value;
}