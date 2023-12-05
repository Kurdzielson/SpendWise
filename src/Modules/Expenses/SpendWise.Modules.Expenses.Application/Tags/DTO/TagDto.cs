namespace SpendWise.Modules.Expenses.Application.Tags.DTO;

internal class TagDto
{
    public Guid TagId { get; init; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string ColorHex { get; set; }
}