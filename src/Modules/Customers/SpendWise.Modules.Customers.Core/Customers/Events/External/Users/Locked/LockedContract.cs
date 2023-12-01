using SpendWise.Shared.Abstraction.Contracts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Locked;

[Message("users")]
internal class LockedContract : Contract<Locked>
{
    public LockedContract()
        => RequireAll();
}