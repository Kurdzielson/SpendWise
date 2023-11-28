using System.Runtime.CompilerServices;
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.Services;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Modules.Users.Core.Users.Events;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Auth;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignIn;

internal class SignInHandler(IUserRepository userRepository, IAuthManager authManager,
        IPasswordHasher<User> passwordHasher,
        IUserRequestStorage userRequestStorage, IMessageBroker messageBroker, ILogger<SignInHandler> logger)
    : ICommandHandler<SignInCommand>
{
    public async Task HandleAsync(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(command.Email.ToLowerInvariant(), cancellationToken)
                   ?? throw new UserNotFoundException(command.Email);

        if (user.State.Code != AvailableUserStates.Active.Code)
            throw new UserNotActiveException(user.Id);
        
        if(passwordHasher.VerifyHashedPassword(default, user.Password, command.Password) == PasswordVerificationResult.Failed)
            throw new InvalidCredentialsException();
        
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };
        
        var jwt = authManager.CreateToken(user.Id, user.Role.Name, claims: claims);
        jwt.Email = user.Email;
        await messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
        logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        userRequestStorage.SetToken(command.Id, jwt);
    }
}