using SpendWise.Modules.Expenses.Core.Expenses.Types;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Amount;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;

namespace SpendWise.Modules.Expenses.Core.Expenses.Entities;

internal class Expense
{
    public ExpenseId Id { get; init; }
    public CustomerId CustomerId { get; set; }
    public Date Date { get; set; }
    public ExpenseAmount Amount { get; set; }
    public Currency Currency { get; set; }
    public ExpenseDescription Description { get; set; }
    public ExpenseCategory Category { get; set; }

    //TODO Attachments
    public List<Guid> TagIds { get; set; } = new();

    //solution to dotnet ef error
    private Expense()
    {
    }

    private Expense(CustomerId customerId, Date date, ExpenseAmount amount, ExpenseDescription description,
        ExpenseCategory category, Currency currency)
    {
        Id = ExpenseId.Create();
        CustomerId = customerId;
        Date = date;
        Amount = amount;
        Description = description;
        Category = category;
        Currency = currency;
    }

    public static Expense Create(Guid customerId, DateTimeOffset date, decimal amount, string description,
        ExpenseCategory category, Currency currency)
        => new(customerId, date, amount, description, category, currency);

    public void AddTags(IEnumerable<Guid> tagIds)
        => TagIds.AddRange(tagIds);

    public void Update(DateTimeOffset date, decimal amount, string description, Currency currency,
        ExpenseCategory category, IEnumerable<Guid> tagIds)
    {
        Date = date;
        Amount = amount;
        Description = description;
        Currency = currency;
        Category = category;

        var tagsToRemove = TagIds.Where(q => !tagIds.Contains(q)).ToList();
        var tagsToAdd = tagIds.Where(q => !TagIds.Contains(q)).ToList();
        
        RemoveTags(tagsToRemove);
        AddTags(tagsToAdd);
    }

    private void RemoveTags(IEnumerable<Guid> tagIds)
    {
        foreach (var tagId in tagIds)
            TagIds.Remove(tagId);
    }
}