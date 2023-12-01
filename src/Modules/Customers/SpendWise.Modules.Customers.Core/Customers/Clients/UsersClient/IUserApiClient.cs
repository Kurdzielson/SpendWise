using SpendWise.Modules.Customers.Core.Customers.Clients.UsersClient.DTO;

namespace SpendWise.Modules.Customers.Core.Customers.Clients.UsersClient;

internal interface IUserApiClient
{
    Task<UserDto> GetAsync(string email, CancellationToken cancellationToken);
}