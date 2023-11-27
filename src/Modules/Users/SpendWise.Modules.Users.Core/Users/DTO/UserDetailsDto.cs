namespace SpendWise.Modules.Users.Core.Users.DTO;

internal class UserDetailsDto : UserDto
{
    public IEnumerable<string> Permissions { get; set; }
}   