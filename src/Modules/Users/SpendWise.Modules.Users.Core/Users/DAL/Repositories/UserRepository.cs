using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;

namespace SpendWise.Modules.Users.Core.Users.DAL.Repositories;

internal class UserRepository(UsersWriteDbContext usersContext) : IUserRepository
{
    private readonly DbSet<User> _users = usersContext.Users;

    public async Task<User> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _users.AsNoTracking().Include(q => q.Role).Where(q => q.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<User> GetAsync(string email, CancellationToken cancellationToken)
        => await _users.AsNoTracking().Include(q => q.Role).Where(q => q.Email == email)
            .SingleOrDefaultAsync(cancellationToken);

    public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken)
    {
        var result = await _users.AddAsync(user, cancellationToken);
        await usersContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var result = _users.Update(user);
        await usersContext.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<bool> DoesExistAsync(string email, CancellationToken cancellationToken)
        => await _users.AsNoTracking().Where(q => q.Email == email).AnyAsync(cancellationToken);
}