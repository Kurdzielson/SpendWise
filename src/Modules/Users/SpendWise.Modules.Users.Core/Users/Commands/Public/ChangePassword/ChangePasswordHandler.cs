using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Commands;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.ChangePassword;

internal class ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ILogger<ChangePasswordHandler> logger) : ICommandHandler<ChangePasswordCommand>
{
    public async Task HandleAsync(ChangePasswordCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(command.UserId, cancellationToken)
            ?? throw new UserNotFoundException(command.UserId);

        if (passwordHasher.VerifyHashedPassword(default, user.Password, command.OldPassword) ==
            PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();

        var password = passwordHasher.HashPassword(default, user.Password);
        
        user.UpdatePassword(password);
        await userRepository.UpdateAsync(user, cancellationToken);
        logger.LogInformation($"User with Id: {user.Id} changed password.");
    }
}