using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class UserIsActive (Guid userId): SpendWiseException($"User with Id: '{userId}' is active.")
{
    public Guid UserId { get; } = userId;
}