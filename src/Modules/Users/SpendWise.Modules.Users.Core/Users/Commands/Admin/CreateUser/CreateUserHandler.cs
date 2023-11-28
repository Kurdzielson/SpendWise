using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Modules.Users.Core.Users.Events;
using SpendWise.Modules.Users.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Messaging;
using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Modules.Users.Core.Users.Commands.Admin.CreateUser;

internal class CreateUserHandler
(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
    RegistrationOptions registrationOptions, IRoleRepository roleRepository,
    IClock clock, ILogger<CreateUserHandler> logger, IMessageBroker messageBroker) : ICommandHandler<CreateUserCommand>
{
    public async Task HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var email = command.Email.ToLowerInvariant();
        var provider = email.Split("@").Last();

        if (registrationOptions.InvalidEmailProviders.Any(q => q.Contains(provider)))
            throw new InvalidEmailException(email);

        if (await userRepository.DoesExistAsync(email, cancellationToken))
            throw new EmailInUseException();

        if (!await roleRepository.DoesExistAsync(command.Role, cancellationToken))
            throw new RoleNotFoundException(command.Role);

        var now = clock.CurrentDateTimeOffset();
        var password = passwordHasher.HashPassword(default, command.Password);
        var user = User.Create(email, password, command.Role, AvailableUserStates.Default, now);

        var userId = await userRepository.AddAsync(user, cancellationToken);
        await messageBroker.PublishAsync(new SignedUp(userId, email, command.Role), cancellationToken);
        logger.LogInformation($"User with ID: '{userId}' has been created.");
    }
}