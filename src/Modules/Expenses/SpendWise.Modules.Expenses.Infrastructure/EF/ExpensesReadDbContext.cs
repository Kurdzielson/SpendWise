using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Read;
using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Read.Model;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read.Model;

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