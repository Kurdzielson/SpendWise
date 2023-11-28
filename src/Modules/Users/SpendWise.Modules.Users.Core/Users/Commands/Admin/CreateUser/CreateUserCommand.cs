
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.CreateUser;

internal record CreateUserCommand(string Email, string Password, string ConfirmPassword, string Role = Role.User) : ICommand;