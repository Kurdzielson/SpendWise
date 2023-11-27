using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignOut;

internal record SignOutCommand(Guid UserId) : ICommand;