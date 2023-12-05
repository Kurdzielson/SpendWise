using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read.Model;

internal class TagReadModel
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string ColorHex { get; set; }
}