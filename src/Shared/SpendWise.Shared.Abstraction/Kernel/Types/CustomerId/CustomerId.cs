namespace SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;

public class CustomerId(Guid value) : TypeId(value)
{
    public static CustomerId CreateFromUser(Guid userId) 
        => new(userId);

    public static implicit operator CustomerId(Guid id)
        => new(id);

    public static implicit operator Guid(CustomerId id)
        => id.Value;
}