using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;

public class Currency
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool IsActive { get; set; }

    public Currency(string code, string name, string symbol, bool isActive)
    {
        if (!IsCodeSupported(code))
            throw new UnsupportedCurrencyCodeException(code);
        Code = code;
        Name = name;
        Symbol = symbol;
        IsActive = isActive;
    }

    private static bool IsCodeSupported(string code)
        => AvailableCurrencyCodes.AllCodes.Contains(code, StringComparer.InvariantCultureIgnoreCase);
}