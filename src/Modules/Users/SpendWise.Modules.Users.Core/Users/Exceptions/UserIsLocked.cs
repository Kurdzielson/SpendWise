using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class UserIsLocked(Guid userId) : SpendWiseException($"User with Id: '{userId}' is locked.")
{
    public Guid UserId { get; } = userId;
}