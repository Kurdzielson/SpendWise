using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;

internal record UnlockUserCommand(Guid UserId) : ICommand;