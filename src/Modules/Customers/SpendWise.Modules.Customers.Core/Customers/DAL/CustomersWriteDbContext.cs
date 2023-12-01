using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Write;
using SpendWise.Modules.Customers.Core.Customers.Domain.Entities;

namespace SpendWise.Modules.Customers.Core.Customers.DAL;

internal class CustomersWriteDbContext(DbContextOptions<CustomersWriteDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customers");
        modelBuilder.ApplyConfiguration(new CustomerWriteConfiguration());
    }
}