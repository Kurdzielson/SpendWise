
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.LockUser;

internal record LockUserCommand(Guid UserId) : ICommand<UpdateResponse>;