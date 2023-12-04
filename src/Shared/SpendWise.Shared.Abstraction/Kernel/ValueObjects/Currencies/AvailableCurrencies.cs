using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies.Exceptions;

namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;

public class AvailableCurrencies
{
    private static readonly Currency USD = new(AvailableCurrencyCodes.USD, "United States Dollar", "$", true);
    private static readonly Currency EUR = new(AvailableCurrencyCodes.EUR, "Euro", "€", true);
    private static readonly Currency GBP = new(AvailableCurrencyCodes.GBP, "British Pound Sterling", "£", true);
    private static readonly Currency JPY = new(AvailableCurrencyCodes.JPY, "Japanese Yen", "¥", true);
    private static readonly Currency CAD = new(AvailableCurrencyCodes.CAD, "Canadian Dollar", "C$", true);
    private static readonly Currency CNY = new(AvailableCurrencyCodes.CNY, "Chinese Yuan", "¥", true);
    private static readonly Currency CHF = new(AvailableCurrencyCodes.CHF, "Swiss Franc", "Fr", true);
    private static readonly Currency AUD = new(AvailableCurrencyCodes.AUD, "Australian Dollar", "A$", true);
    private static readonly Currency HKD = new(AvailableCurrencyCodes.HKD, "Hong Kong Dollar", "HK$", true);
    private static readonly Currency SEK = new(AvailableCurrencyCodes.SEK, "Swedish Krona", "kr", true);
    private static readonly Currency NOK = new(AvailableCurrencyCodes.NOK, "Norwegian Krone", "kr", true);
    private static readonly Currency PLN = new(AvailableCurrencyCodes.PLN, "Polish Zloty", "zł", true);

    public static readonly Currency DefaultCurrency = EUR;

    private static readonly HashSet<Currency> AllCurrencies = new()
    {
        USD, EUR, GBP, JPY, CAD, CNY, CHF, AUD, HKD, SEK, NOK, PLN
    };

    public static Currency GetCurrency(string code)
    {
        var currency =
            AllCurrencies.FirstOrDefault(q => q.Code.Contains(code, StringComparison.InvariantCultureIgnoreCase));

        if (currency is null)
            throw new UnsupportedCurrencyCodeException(code);

        return currency;
    }

    public static List<Currency> GetAllList()
        => AllCurrencies.ToList();
}