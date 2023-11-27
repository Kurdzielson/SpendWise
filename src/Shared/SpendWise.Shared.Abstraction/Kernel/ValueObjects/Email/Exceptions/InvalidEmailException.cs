using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Email.Exceptions;

public class InvalidEmailException(string email) : SpendWiseException($"Email: '{email}' is invalid.")
{
    public string Email { get; } = email;
}