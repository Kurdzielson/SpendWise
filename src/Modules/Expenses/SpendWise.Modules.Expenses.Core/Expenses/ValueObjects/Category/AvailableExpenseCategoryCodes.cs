namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;

internal class AvailableExpenseCategoryCodes
{
    public static readonly string Rent = nameof(Rent);
    public static readonly string Utilities = nameof(Utilities);
    public static readonly string Transportation = nameof(Transportation);
    public static readonly string Foods = nameof(Foods);
    public static readonly string Entertainment = nameof(Entertainment);
    public static readonly string Insurance = nameof(Insurance);
    public static readonly string Taxes = nameof(Taxes);
    public static readonly string Healthcare = nameof(Healthcare);
    public static readonly string Education = nameof(Education);
    public static readonly string Salaries = nameof(Salaries);
    public static readonly string Office = nameof(Office);
    public static readonly string Other = nameof(Other);

    public static readonly IReadOnlyCollection<string> AllCodes = new List<string>()
    {
        Rent, Utilities, Transportation, Foods, Entertainment, Insurance, Taxes, Healthcare, Education, Salaries, Office, Other
    };
}