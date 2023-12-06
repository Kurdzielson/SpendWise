using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category.Exceptions;

namespace SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;

internal class AvailableExpenseCategories
{
    private static readonly ExpenseCategory Rent = new(AvailableExpenseCategoryCodes.Rent, "Rent/Mortgage",
        "This is the cost of housing. For businesses, this includes the cost of office or retail space.");

    private static readonly ExpenseCategory Utilities = new(AvailableExpenseCategoryCodes.Utilities, "Utilities",
        "These are costs for services like electricity, gas, internet, etc.");

    private static readonly ExpenseCategory Transportation = new(AvailableExpenseCategoryCodes.Transportation,
        "Transportation",
        "In personal finance, this is often the cost of a car, public transportation, fuel, and related expenses. For businesses, this can involve costs for shipping, distribution, and fleet maintenance.");

    private static readonly ExpenseCategory Foods = new(AvailableExpenseCategoryCodes.Foods,
        "Foods and Groceries", "This includes the cost for meals, both eating out and cooking at home.");

    private static readonly ExpenseCategory Entertainment = new(AvailableExpenseCategoryCodes.Entertainment,
        "Entertainment",
        "This category includes expenses for activities like going to the movies, concerts, and other forms of entertainment.");

    private static readonly ExpenseCategory Insurance = new(AvailableExpenseCategoryCodes.Insurance, "Insurance",
        "This can include several types of insurance such as health, vehicle, life, property, and in some cases, professional or business liability insurance.");

    private static readonly ExpenseCategory Taxes = new(AvailableExpenseCategoryCodes.Taxes, "Taxes",
        "This includes income tax, business tax, sales tax, etc.");

    private static readonly ExpenseCategory Healthcare = new(AvailableExpenseCategoryCodes.Healthcare, "Healthcare",
        "This covers health-related expenses, including surgeries, medications, and health insurance premiums.");

    private static readonly ExpenseCategory Education = new(AvailableExpenseCategoryCodes.Education, "Education",
        "These are the costs associated with schooling, college, or any type of educational courses or trainings.");

    private static readonly ExpenseCategory Salaries = new(AvailableExpenseCategoryCodes.Salaries,
        "Salaries and Wages", "For businesses, this involves all costs associated with employee compensation.");

    private static readonly ExpenseCategory Office = new(AvailableExpenseCategoryCodes.Office,
        "Office Supplies/Equipment",
        "These are costs associated with running an office, like the cost of computers, desks, etc.");

    private static readonly ExpenseCategory Other = new(AvailableExpenseCategoryCodes.Other,
        "Other",
        "These are costs that do not fit under other specified categories.");

    private static readonly HashSet<ExpenseCategory> AllCategories = new()
    {
        Rent, Utilities, Transportation, Foods, Entertainment, Insurance, Taxes, Healthcare, Education, Salaries,
        Office, Other
    };

    public static ExpenseCategory GetCategory(string code)
    {
        var category =
            AllCategories.FirstOrDefault(q => q.Code.Contains(code, StringComparison.InvariantCultureIgnoreCase));

        if (category is null)
            throw new UnsupportedExpenseCategoryCodeException(code);

        return category;
    }

    public static List<ExpenseCategory> GetList()
        => AllCategories.ToList();
}