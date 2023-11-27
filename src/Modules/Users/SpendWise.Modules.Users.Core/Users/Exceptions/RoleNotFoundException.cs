using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Modules.Users.Core.Users.Exceptions;

internal class RoleNotFoundException(string role) : SpendWiseException($"Role: '{role}' was not found.")
{
    public string Role { get; } = role;
}