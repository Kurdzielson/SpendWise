using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Shared.Abstraction.Kernel.Enums;

namespace SpendWise.Modules.Users.Core.Users.DTO;

internal class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public UserState State { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
}