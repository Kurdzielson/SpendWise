namespace SpendWise.Shared.Abstraction.Exceptions;

public abstract class SpendWiseException : Exception
{
    protected SpendWiseException(string message) : base(message)
    {
    }
}