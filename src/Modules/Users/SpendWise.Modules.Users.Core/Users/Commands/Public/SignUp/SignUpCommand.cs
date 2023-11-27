using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignUp;

internal record SignUpCommand(string Email, string Password, string ConfirmPassword) : ICommand;