using SpendWise.Modules.Customers.Core.Users.Clients.DTO;

namespace SpendWise.Modules.Customers.Core.Users.Clients;

internal interface IUserApiClient
{
    Task<UserDto> GetAsync(string email, CancellationToken cancellationToken);
}