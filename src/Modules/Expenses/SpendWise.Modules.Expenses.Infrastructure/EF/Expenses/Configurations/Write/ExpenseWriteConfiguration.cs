using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Expenses.Types;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Amount;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Category;
using SpendWise.Modules.Expenses.Core.Expenses.ValueObjects.Description;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Currencies;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Expenses.Configurations.Write;

internal class ExpenseWriteConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses");

        builder.HasKey(q => q.Id);

        builder
            .Property<ExpenseId>("Id")
            .HasConversion(q => q.Value, q => new ExpenseId(q));

        builder
            .Property<CustomerId>("CustomerId")
            .HasConversion(q => q.Value, q => new CustomerId(q));

        builder
            .Property<Date>("Date")
            .HasConversion(q => q.Value, q => new Date(q));

        builder
            .Property<ExpenseAmount>("Amount")
            .HasConversion(q => q.Value, q => new ExpenseAmount(q));

        builder
            .Property<ExpenseDescription>("Description")
            .HasConversion(q => q.Value, q => new ExpenseDescription(q));

        builder
            .Property<ExpenseCategory>("Category")
            .HasConversion(q => q.Code, q => AvailableExpenseCategories.GetCategory(q));

        builder
            .Property<Currency>("Currency")
            .HasConversion(q => q.Code, q => AvailableCurrencies.GetCurrency(q));

        builder.HasMany(q => q.Tags)
            .WithMany(q => q.Expenses);
    }
}