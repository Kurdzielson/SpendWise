using SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read.Model;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Modules.Users.Core.Users.DTO;

namespace SpendWise.Modules.Users.Core.Users.Queries;

internal static class Extensions
{
    
    public static UserDto AsDto(this UserReadModel member)
        => member.Map<UserDto>();

    public static UserDetailsDto AsDetailsDto(this UserReadModel user)
    {
        var dto = user.Map<UserDetailsDto>();
        dto.Permissions = user.Role.Permissions;

        return dto;
    }
    
    private static T Map<T>(this UserReadModel user) where T : UserDto, new()
        => new()
        {
            UserId = user.Id,
            Email = user.Email,
            State = AvailableUserStates.GetState(user.State),
            Role = user.Role?.Name,
            CreatedAt = user.CreatedAt
        };
}