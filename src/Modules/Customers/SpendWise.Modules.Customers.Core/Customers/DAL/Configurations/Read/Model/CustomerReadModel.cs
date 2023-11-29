namespace SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read.Model;

internal class CustomerReadModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nick { get; set; }
    public string FullName { get; set; }
    public string State { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? VerifiedAt { get; set; }
}