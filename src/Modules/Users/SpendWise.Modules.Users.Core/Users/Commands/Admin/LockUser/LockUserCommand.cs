
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.LockUser;

internal record LockUserCommand(Guid UserId) : ICommand;