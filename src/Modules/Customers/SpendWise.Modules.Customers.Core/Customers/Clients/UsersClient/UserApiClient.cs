using SpendWise.Modules.Customers.Core.Customers.Clients.UsersClient.DTO;
using SpendWise.Shared.Abstraction.Modules;

namespace SpendWise.Modules.Customers.Core.Customers.Clients.UsersClient;

internal class UserApiClient(IModuleClient client) : IUserApiClient
{
    public async Task<UserDto> GetAsync(string email, CancellationToken cancellationToken)
        => await client.SendAsync<UserDto>("users/get", new { email }, cancellationToken);
}