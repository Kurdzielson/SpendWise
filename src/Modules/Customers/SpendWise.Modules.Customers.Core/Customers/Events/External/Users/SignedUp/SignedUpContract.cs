using SpendWise.Shared.Abstraction.Contracts;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.SignedUp;

[Message("users")]
internal class SignedUpContract : Contract<SignedUp>
{
    public SignedUpContract()
        => RequireAll();
}