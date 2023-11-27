using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick.Exceptions;

public class InvalidNickException(string nick) : SpendWiseException($"Nick: '{nick}' is invalid.")
{
    public string Nick { get; } = nick;
}