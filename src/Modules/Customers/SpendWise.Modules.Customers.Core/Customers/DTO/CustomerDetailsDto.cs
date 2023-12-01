namespace SpendWise.Modules.Customers.Core.Customers.DTO;

internal class CustomerDetailsDto : CustomerDto
{
    public DateTime? CreatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? VerifiedAt { get; set; }
}