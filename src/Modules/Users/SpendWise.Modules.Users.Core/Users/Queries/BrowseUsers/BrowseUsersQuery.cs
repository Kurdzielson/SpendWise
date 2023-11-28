using SpendWise.Modules.Users.Core.Users.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Users.Core.Users.Queries.BrowseUsers;

internal class BrowseUsersQuery() : PagedQuery<UserDto>
{
    public string State { get; set; }
    public string Role { get; set; }
    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }
    internal new string OrderBy { get; set; } //hide
}