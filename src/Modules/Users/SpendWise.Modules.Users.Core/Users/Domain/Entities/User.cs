using Microsoft.AspNetCore.Identity;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Shared.Abstraction.Kernel.Types.UserId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Email;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Password;

namespace SpendWise.Modules.Users.Core.Users.Domain.Entities;

internal class User
{
    public UserId Id { get; set; }
    
    public Email Email { get; set; }
    public Password Password { get; set; }

    public string RoleId { get; set; }
    public Role Role { get; set; }

    public UserState State { get; set; }
    public Date CreatedAt { get; set; }

    //solution to dotnet ef bug
    private User() {}

    private User(Email email, Password password, string roleId, UserState state, Date createdAt)
    {
        Id = UserId.Create();
        Email = email;
        Password = password;
        RoleId = roleId;
        State = state;
        CreatedAt = createdAt;
    }

    public static User Create(Email email, Password password, string roleId, UserState userState, Date createdAt)
        => new(email, password, roleId, userState, createdAt);

    public void UpdatePassword(Password newPassword)
        => Password = newPassword;
}