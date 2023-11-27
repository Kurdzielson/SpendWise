using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignIn;

internal record SignInCommand(string Email, string Password) : ICommand
{
    internal Guid Id { get; init; } = Guid.NewGuid();
}