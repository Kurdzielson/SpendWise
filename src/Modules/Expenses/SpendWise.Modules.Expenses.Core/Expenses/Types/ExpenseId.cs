using SpendWise.Shared.Abstraction.Kernel.Types;

namespace SpendWise.Modules.Expenses.Core.Expenses.Types;

internal class ExpenseId(Guid value) : TypeId(value)
{
    public static ExpenseId Create()
        => new(Guid.NewGuid());
    
    public static implicit operator ExpenseId(Guid id)
        => new(id);
    
    public static implicit operator Guid(ExpenseId id)
        => id.Value;
}