using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Expenses.Read;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Expenses.Read.Model;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Read;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF;

internal class ExpensesReadDbContext : DbContext
{
    public DbSet<ExpenseReadModel> Expenses { get; set; }
    public DbSet<TagReadModel> Tags { get; set; }

    public ExpensesReadDbContext(DbContextOptions<ExpensesReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("expenses");

        modelBuilder.ApplyConfiguration(new ExpenseReadConfiguration());
        modelBuilder.ApplyConfiguration(new TagReadConfiguration());
    }
}