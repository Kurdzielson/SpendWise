
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.Responses;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.CreateUser;

internal record CreateUserCommand(string Email, string Password, string ConfirmPassword, string Role = Role.User) : ICommand<CreateResponse>;