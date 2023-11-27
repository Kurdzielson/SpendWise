using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class InvalidCredentialsException() : SpendWiseException("Invalid credentials.")
{
}