using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;

internal class ExpenseCategory
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ExpenseCategory(string code, string name, string description)
    {
        if (!IsCodeSupported(code))
            throw new UnsupportedExpenseCategoryCodeException(code);

        Code = code;
        Name = name;
        Description = description;
    }

    private static bool IsCodeSupported(string code)
        => AvailableCurrencyCodes.AllCodes.Contains(code, StringComparer.InvariantCultureIgnoreCase);
}