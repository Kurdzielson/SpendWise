namespace SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Read.Model;

internal class ExpenseReadModel
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Currency { get; set; }
}