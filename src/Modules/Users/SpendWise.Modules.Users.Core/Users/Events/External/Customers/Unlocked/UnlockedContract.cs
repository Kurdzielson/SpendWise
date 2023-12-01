using SpendWise.Shared.Abstraction.Contracts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Users.Core.Users.Events.External.Customers.Unlocked;

[Message("customers")]
internal class UnlockedContract : Contract<Unlocked>
{
    public UnlockedContract()
        => RequireAll();
}