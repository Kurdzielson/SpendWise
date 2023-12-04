namespace SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Read.Model;

internal class TagReadModel
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string ColorHex { get; set; }
}