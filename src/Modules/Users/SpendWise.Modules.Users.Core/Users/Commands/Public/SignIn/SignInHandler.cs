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

internal class SignInHandler : ICommandHandler<SignInCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authManager;
    private readonly IUserRequestStorage _userRequestStorage;
    private readonly IMessageBroker _messageBroker;
    private readonly ILogger<SignInHandler> _logger;

    public SignInHandler(IUserRepository userRepository, IAuthManager authManager,
        IPasswordHasher<User> passwordHasher,
        IUserRequestStorage userRequestStorage, IMessageBroker messageBroker, ILogger<SignInHandler> logger)
    {
        _userRepository = userRepository;
        _authManager = authManager;
        _userRequestStorage = userRequestStorage;
        _messageBroker = messageBroker;
        _logger = logger;
    }

    public async Task HandleAsync(SignInCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(command.Email.ToLowerInvariant(), cancellationToken)
                   ?? throw new UserNotFoundException(command.Email);

        if (user.State.Code != AvailableUserStates.Active.Code)
            throw new UserNotActiveException(user.Id);

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };

        var jwt = _authManager.CreateToken(user.Id, user.Role.Name, claims: claims);
        jwt.Email = user.Email;
        await _messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
        _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        _userRequestStorage.SetToken(command.Id, jwt);
    }
}