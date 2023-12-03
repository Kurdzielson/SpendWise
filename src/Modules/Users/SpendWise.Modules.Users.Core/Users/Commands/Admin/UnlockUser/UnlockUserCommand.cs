using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.UnlockUser;

internal record UnlockUserCommand(Guid UserId) : ICommand<UpdateResponse>;