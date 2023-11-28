using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read;
using SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read.Model;

namespace SpendWise.Modules.Users.Core.Users.DAL;

internal class UsersReadDbContext : DbContext
{
    public DbSet<RoleReadModel> Roles { get; set; }
    public DbSet<UserReadModel> Users { get; set; }

    public UsersReadDbContext(DbContextOptions<UsersReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfiguration(new RoleReadConfiguration());
        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
    }
}