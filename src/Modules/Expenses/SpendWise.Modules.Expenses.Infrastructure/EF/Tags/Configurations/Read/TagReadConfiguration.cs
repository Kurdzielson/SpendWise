using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read;

internal class TagReadConfiguration : IEntityTypeConfiguration<TagReadModel>
{
    public void Configure(EntityTypeBuilder<TagReadModel> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(q => q.Id);
    }
}