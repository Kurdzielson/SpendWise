using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.Repositories;

namespace SpendWise.Modules.Users.Core.Users.DAL.Repositories;

internal class RoleRepository(UsersWriteDbContext usersContext) : IRoleRepository
{
    private readonly UsersWriteDbContext _usersContext = usersContext;
    private readonly DbSet<Role> _roles = usersContext.Roles;

    public Task<Role?> GetAsync(string name, CancellationToken cancellationToken) =>
        _roles.AsNoTracking().Where(x => x.Name == name).SingleOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken) =>
        await _roles.AsNoTracking().ToListAsync(cancellationToken);

    public Task<bool> DoesExistAsync(string name, CancellationToken cancellationToken)
        => _roles.AsNoTracking().Where(q => q.Name == name).AnyAsync(cancellationToken);
}