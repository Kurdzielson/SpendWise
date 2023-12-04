using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies.Exceptions;

internal class UnsupportedCurrencyCodeException(string code)
    : SpendWiseException("Unsupported Currency Code: '{code}'.")
{
    public string Code { get; } = code;
}