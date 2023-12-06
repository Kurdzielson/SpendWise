using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Write;

namespace SpendWise.Modules.Expenses.Infrastructure.EF;

internal class ExpensesWriteDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public ExpensesWriteDbContext(DbContextOptions<ExpensesWriteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("expenses");

        modelBuilder.ApplyConfiguration(new ExpenseWriteConfiguration());
    }
}