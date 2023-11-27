using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.ChangePassword;

internal record ChangePasswordCommand(string OldPassword, string NewPassword, string ConfirmNewPassword) : ICommand
{
    internal Guid UserId { get; set; }
}