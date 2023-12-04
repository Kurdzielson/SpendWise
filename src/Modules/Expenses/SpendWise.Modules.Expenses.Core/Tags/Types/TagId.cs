using SpendWise.Shared.Abstraction.Kernel.Types;

namespace SpendWise.Modules.Expenses.Core.Tags.Types;

internal class TagId(Guid value) : TypeId(value)
{
    public static TagId Create()
        => new(Guid.NewGuid());

    public static implicit operator TagId(Guid id)
        => new(id);

    public static implicit operator Guid(TagId id)
        => id.Value;
}