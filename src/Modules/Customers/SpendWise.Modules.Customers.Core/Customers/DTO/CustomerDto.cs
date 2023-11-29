namespace SpendWise.Modules.Customers.Core.Customers.DTO;

internal class CustomerDto
{
    public Guid CustomerId { get; set; }
    public string Email { get; set; }
    public string Nick { get; set; }
    public string State { get; set; }
    public string FullName { get; set; }
}