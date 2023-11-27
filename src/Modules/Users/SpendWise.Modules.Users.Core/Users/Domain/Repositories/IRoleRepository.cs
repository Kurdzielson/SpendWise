using SpendWise.Modules.Users.Core.Users.Domain.Entities;

namespace SpendWise.Modules.Users.Core.Users.Domain.Repositories;

internal interface IRoleRepository
{
    Task<Role?> GetAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> DoesExistAsync(string name, CancellationToken cancellationToken);
}