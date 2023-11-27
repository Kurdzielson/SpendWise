using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class UserNotFoundException : SpendWiseException
{

    public UserNotFoundException(Guid userId) : this($"User with Id: '{userId}' not found")
    {
    }

    public UserNotFoundException(string email) : base($"User with email: '{email}' not found")
    {
    }
}