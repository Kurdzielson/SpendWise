namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.CreatedAt;

public class CreatedAt(DateTime? value) : ValueObject
{
    public DateTime? Value { get; } = value;

    public static implicit operator CreatedAt?(DateTime? value) => value is null ? null : new CreatedAt(value);
    public static implicit operator DateTime?(CreatedAt value) => value?.Value;
}