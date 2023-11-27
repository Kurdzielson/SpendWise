using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class InvalidEmailException(string email) : SpendWiseException($"State is invalid: '{email}'.")
{
    public string Email { get; } = email;
}