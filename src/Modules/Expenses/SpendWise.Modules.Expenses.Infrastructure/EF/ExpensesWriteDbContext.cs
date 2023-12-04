using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Entities;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Expenses.Write;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Write;

namespace SpendWise.Modules.Expenses.Infrastructure.EF;

internal class ExpensesWriteDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public ExpensesWriteDbContext(DbContextOptions<ExpensesWriteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("expenses");

        modelBuilder.ApplyConfiguration(new ExpenseWriteConfiguration());
        modelBuilder.ApplyConfiguration(new TagWriteConfiguration());
    }
}