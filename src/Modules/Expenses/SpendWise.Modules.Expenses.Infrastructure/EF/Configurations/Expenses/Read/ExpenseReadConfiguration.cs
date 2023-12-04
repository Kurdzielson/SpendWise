using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Expenses.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Expenses.Read;

internal class ExpenseReadConfiguration : IEntityTypeConfiguration<ExpenseReadModel>
{
    public void Configure(EntityTypeBuilder<ExpenseReadModel> builder)
    {
        builder.ToTable("Expenses");

        builder.HasKey(q => q.Id);
    }
}