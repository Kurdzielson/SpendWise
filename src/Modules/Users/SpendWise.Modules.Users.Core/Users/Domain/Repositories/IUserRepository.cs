using SpendWise.Modules.Users.Core.Users.Domain.Entities;

namespace SpendWise.Modules.Users.Core.Users.Domain.Repositories;

internal interface IUserRepository
{
    Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<User> GetAsync(string email, CancellationToken cancellationToken);
    Task<Guid> AddAsync(User user, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<bool> DoesExistAsync(string email, CancellationToken cancellationToken);
}