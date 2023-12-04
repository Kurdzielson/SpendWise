using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Read;

internal class TagReadConfiguration : IEntityTypeConfiguration<TagReadModel>
{
    public void Configure(EntityTypeBuilder<TagReadModel> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(q => q.Id);
    }
}