using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class UserNotActiveException(Guid userId) : SpendWiseException($"User with ID: '{userId}' is not active.")
{
    public Guid UserId { get; } = userId;
}