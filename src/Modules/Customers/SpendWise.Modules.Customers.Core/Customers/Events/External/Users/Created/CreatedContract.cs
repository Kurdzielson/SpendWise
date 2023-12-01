using SpendWise.Shared.Abstraction.Contracts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Created;

[Message("users")]
internal class CreatedContract : Contract<Created>
{
    public CreatedContract()
        => RequireAll();
}