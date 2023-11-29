using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read;
using SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read.Model;

namespace SpendWise.Modules.Customers.Core.Customers.DAL;

internal class CustomersReadDbContext(DbContextOptions<CustomersReadDbContext> options) : DbContext(options)
{
    public DbSet<CustomerReadModel> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customers");
        modelBuilder.ApplyConfiguration(new CustomerReadConfiguration());
    }
}