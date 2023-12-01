using SpendWise.Modules.Customers.Core.Customers.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Customers.Core.Customers.Queries.BrowseCustomers;

internal class BrowseCustomersQuery : PagedQuery<CustomerDto>
{
    public string State { get; init; }
    public string Email { get; init; }
    public string Nick { get; init; }
    public DateTime? CreatedAtFrom { get; init; }
    public DateTime? CreatedAtTo { get; init; }
    public DateTimeOffset? CompletedAtFrom { get; init; }
    public DateTimeOffset? CompletedAtTo { get; init; }
    public DateTimeOffset? VerifiedAtFrom { get; init; }
    public DateTimeOffset? VerifiedAtTo { get; init; }
    internal new string OrderBy { get; init; } //hide
}