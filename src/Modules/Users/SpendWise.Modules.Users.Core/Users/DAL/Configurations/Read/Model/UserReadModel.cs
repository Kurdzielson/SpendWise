using SpendWise.Modules.Users.Core.DAL.Configurations.Read.Model;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;

namespace SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read.Model;

internal class UserReadModel
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public RoleReadModel Role { get; init; }
    public string RoleId { get;  init; }
    public string State { get; init; }
    public DateTime? CreatedAt { get; init; }
}