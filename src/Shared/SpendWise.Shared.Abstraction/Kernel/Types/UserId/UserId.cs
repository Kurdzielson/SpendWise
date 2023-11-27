namespace SpendWise.Shared.Abstraction.Kernel.Types.UserId;

public class UserId : TypeId
{
    public UserId(Guid value) : base(value)
    {
    }

    public static UserId Create()
        => new(Guid.NewGuid());
    
    public static implicit operator UserId(Guid id)
        => new(id);
    
    public static implicit operator Guid(UserId id)
        => id.Value;
}