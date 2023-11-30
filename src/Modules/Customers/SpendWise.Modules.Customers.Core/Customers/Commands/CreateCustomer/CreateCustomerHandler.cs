using Microsoft.Extensions.Logging;
using SpendWise.Modules.Customers.Core.Customers.Domain.Entities;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Modules.Customers.Core.Users.Clients;
using SpendWise.Modules.Customers.Core.Users.Exceptions;
using SpendWise.Shared.Abstraction.Commands;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Email;
using SpendWise.Shared.Abstraction.Time;

namespace SpendWise.Modules.Customers.Core.Customers.Commands.CreateCustomer;

internal class CreateCustomerHandler(ICustomerRepository customerRepository, IUserApiClient userApiClient,
    ILogger<CreateCustomerHandler> logger, IClock clock) : ICommandHandler<CreateCustomerCommand>
{
    private const string ValidRole = "user";

    public async Task HandleAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        _ = new Email(command.Email);
        var user = await userApiClient.GetAsync(command.Email, cancellationToken)
                   ?? throw new UserNotFoundException(command.Email);

        if (user.Role is not ValidRole)
            return;

        if (await customerRepository.DoesExistAsync(user.UserId, cancellationToken))
            throw new CustomerAlreadyExistException(user.UserId);

        var now = clock.CurrentDate();
        var customer = Customer.CreateFromUser(user.UserId, user.Email, now);

        await customerRepository.AddAsync(customer, cancellationToken);
        logger.LogInformation($"Created a customer based on User with Id: '{user.UserId}'");
    }
}