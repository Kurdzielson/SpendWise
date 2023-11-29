using SpendWise.Shared.Abstraction.Kernel.Enums;

namespace SpendWise.Modules.Customers.Core.Users.Clients.DTO;

internal class UserDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}