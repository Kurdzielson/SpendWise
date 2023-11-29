using SpendWise.Shared.Abstraction.Kernel.Types;

namespace SpendWise.Modules.Customers.Core.Customers.Domain.Types;

internal class CustomerId(Guid value) : TypeId(value)
{
    public CustomerId CreateFromUser(Guid userId) => new CustomerId(userId);
}