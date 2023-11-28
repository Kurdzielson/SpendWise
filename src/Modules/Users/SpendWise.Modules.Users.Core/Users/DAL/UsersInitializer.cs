using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpendWise.Modules.Users.Core.DAL;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Shared.Infrastructure;

namespace SpendWise.Modules.Users.Core.Users.DAL;

internal sealed class UsersInitializer(UsersWriteDbContext dbContext, ILogger<UsersInitializer> logger)
    : IInitializer
{
    private const string Users = "users";

    private static readonly HashSet<string> UserPermissions = new ()
    {
        
    };

    private static readonly HashSet<string> AdminPermissions = new (UserPermissions)
    {
        Users
    };

    private readonly IEnumerable<Role> _roles = new List<Role>()
    {
        Role.Create(Role.User, UserPermissions),
        Role.Create(Role.Admin, AdminPermissions)
    };
    
    public async Task InitAsync()
    {
        if (await dbContext.Roles.AnyAsync())
        {
            //TODO remove after development
            await UpdateAsync();
            return;
        }

        await AddRolesAsync();
        await dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        foreach (var role in _roles)
            await dbContext.Roles.AddAsync(role);
        

        logger.LogInformation("Initialized roles.");
    }

    private async Task UpdateAsync()
    {
        foreach (var role in _roles)
        {
            dbContext.UpdateRange(_roles);
        }

        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updated roles.");
    }
    
}