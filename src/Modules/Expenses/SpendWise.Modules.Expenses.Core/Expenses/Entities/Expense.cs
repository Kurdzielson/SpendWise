using SpendWise.Modules.Expenses.Core.Expenses.Types;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Amount;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Name;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;

namespace SpendWise.Modules.Expenses.Core.Expenses.Entities;

internal class Expense
{
    public ExpenseId Id { get; init; }
    public CustomerId CustomerId { get; set; }
    public ExpenseName Name { get; set; }
    public ExpenseAmount Amount { get; set; }
    public Currency Currency { get; set; }
    public ExpenseDescription Description { get; set; }
    public Date Date { get; set; }
    public ExpenseCategory Category { get; set; }

    //solution to dotnet ef error
    private Expense()
    {
    }

    private Expense(CustomerId customerId, ExpenseName name, Date date, ExpenseAmount amount,
        ExpenseDescription description, ExpenseCategory category, Currency currency)
    {
        Id = ExpenseId.Create();
        Name = name;
        CustomerId = customerId;
        Date = date;
        Amount = amount;
        Description = description;
        Category = category;
        Currency = currency;
    }

    public static Expense Create(Guid customerId, string name, DateTimeOffset date, decimal amount, string description,
        ExpenseCategory category, Currency currency)
        => new(customerId, name, date, amount, description, category, currency);

    public void Update(DateTimeOffset date, string name, decimal amount, string description, Currency currency,
        ExpenseCategory category)
    {
        Date = date;
        Name = name;
        Amount = amount;
        Description = description;
        Currency = currency;
        Category = category;
    }
}