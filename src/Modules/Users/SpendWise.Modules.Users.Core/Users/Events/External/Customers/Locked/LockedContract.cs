using SpendWise.Shared.Abstraction.Contracts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Users.Core.Users.Events.External.Customers.Locked;

[Message("customers")]
internal class LockedContract : Contract<Locked>
{
    public LockedContract()
        => RequireAll();
}