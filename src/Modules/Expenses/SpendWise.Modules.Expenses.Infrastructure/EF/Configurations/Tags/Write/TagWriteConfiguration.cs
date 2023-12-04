using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Polly.Bulkhead;
using SpendWise.Modules.Expenses.Core.Tags.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Types;
using SpendWise.Modules.Expenses.Core.Tags.ValueObjects.ColorHex;
using SpendWise.Modules.Expenses.Core.Tags.ValueObjects.Name;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Configurations.Tags.Write;

internal class TagWriteConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(q => q.Id);

        builder
            .Property<TagId>("Id")
            .HasConversion(q => q.Value, q => new TagId(q));

        builder
            .Property<CustomerId>("CustomerId")
            .HasConversion(q => q.Value, q => new CustomerId(q));

        builder
            .Property<TagName>("Name")
            .HasConversion(q => q.Value, q => new TagName(q));

        builder
            .Property<TagColorHex>("ColorHex")
            .HasConversion(q => q.Value, q => new TagColorHex(q));
    }
}