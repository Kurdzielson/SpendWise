using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Expenses.Read.Model;

internal class ExpenseReadModel
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Currency { get; set; }
    public IEnumerable<TagReadModel> Tags { get; set; }
}