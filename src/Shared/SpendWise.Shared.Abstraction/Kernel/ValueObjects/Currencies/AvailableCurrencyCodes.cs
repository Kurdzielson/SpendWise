namespace SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;

public class AvailableCurrencyCodes
{
    public static readonly string USD = nameof(USD); 
    public static readonly string EUR = nameof(EUR); // Euro
    public static readonly string GBP = nameof(GBP); // British Pound Sterling
    public static readonly string JPY = nameof(JPY); // Japanese Yen
    public static readonly string CAD = nameof(CAD); // Canadian Dollar
    public static readonly string CNY = nameof(CNY); // Chinese Yuan
    public static readonly string CHF = nameof(CHF); // Swiss Franc
    public static readonly string AUD = nameof(AUD); // Australian Dollar
    public static readonly string HKD = nameof(HKD); // Hong Kong Dollar
    public static readonly string SEK = nameof(SEK); // Swedish Krona
    public static readonly string NOK = nameof(NOK); // Norwegian Krone
    public static readonly string PLN = nameof(PLN); // Polish Zloty

    public static readonly IReadOnlyCollection<string> AllCodes = new List<string>()
    {
        USD, EUR, GBP, JPY, CAD, CNY, CHF, AUD, HKD, SEK, NOK, PLN
    };
}