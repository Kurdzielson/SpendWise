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

namespace SpendWise.Modules.Users.Core.Users.Commands.Public.SignUp;

internal class SignUpHandler(IUserRepository userRepository, IRoleRepository roleRepository,
        IPasswordHasher<User> passwordHasher, IClock clock, IMessageBroker messageBroker,
        RegistrationOptions registrationOptions, ILogger<SignUpHandler> logger)
    : ICommandHandler<SignUpCommand>
{
    public async Task HandleAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        if (!registrationOptions.Enabled)
            throw new SignUpDisabledException();

        var email = command.Email.ToLowerInvariant();
        var provider = email.Split("@").Last();

        if (registrationOptions.InvalidEmailProviders.Any(q => q.Contains(provider)))
            throw new InvalidEmailException(email);

        if (await userRepository.DoesExistAsync(email, cancellationToken))
            throw new EmailInUseException();

        var roleName = Role.Default;
        if (!await roleRepository.DoesExistAsync(roleName, cancellationToken))
            throw new RoleNotFoundException(roleName);

        var now = clock.CurrentDateTimeOffset();
        var password = passwordHasher.HashPassword(default, command.Password);
        var user = User.Create(email, password, roleName, AvailableUserStates.Default, now);

        await userRepository.AddAsync(user, cancellationToken);
        await messageBroker.PublishAsync(new SignedUp(user.Id, email, roleName), cancellationToken);
        logger.LogInformation($"User with ID: '{user.Id}' has singed up");
    }
}