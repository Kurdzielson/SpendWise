using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class UserNotFoundException(string email) : SpendWiseException($"User with email: '{email}' not found")
{
    public string Email { get; set; } = email;
}