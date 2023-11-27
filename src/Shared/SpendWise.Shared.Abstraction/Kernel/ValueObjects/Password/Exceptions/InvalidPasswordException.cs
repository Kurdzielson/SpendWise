using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Password.Exceptions;

public class InvalidPasswordException(string password) : SpendWiseException($"Password: '{password}' is invalid.")
{
    public string Password { get; } = password;
}