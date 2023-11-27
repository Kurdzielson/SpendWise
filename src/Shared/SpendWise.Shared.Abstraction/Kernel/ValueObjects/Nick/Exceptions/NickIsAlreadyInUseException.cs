using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick.Exceptions;

public class NickIsAlreadyInUseException(string nick) : SpendWiseException($"Nick: {nick} is already in use.")
{
    public string Nick { get; set; } = nick;
}