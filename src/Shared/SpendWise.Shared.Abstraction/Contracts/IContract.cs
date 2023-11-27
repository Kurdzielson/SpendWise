namespace SpendWise.Shared.Abstraction.Contracts;

public interface IContract
{
    Type Type { get; }
    public IEnumerable<string> Required { get; }
}