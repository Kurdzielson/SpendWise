using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.FullName.Exceptions;

public class InvalidFullNameException(string fullName) : SpendWiseException($"Full name: '{fullName}' is invalid.")
{
    public string FullName { get; } = fullName;
}