using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.DAL.Configurations.Write;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;

namespace SpendWise.Modules.Users.Core.Users.DAL;

internal class UsersWriteDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    public UsersWriteDbContext(DbContextOptions<UsersWriteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfiguration(new RoleWriteConfiguration());
        modelBuilder.ApplyConfiguration(new UserWriteConfiguration());
    }
}