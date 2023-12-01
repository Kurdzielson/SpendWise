using SpendWise.Shared.Abstraction.Contracts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Unlocked;

[Message("users")]
internal class UnlockedContract : Contract<Unlocked>
{
    public UnlockedContract()
        => RequireAll();
}