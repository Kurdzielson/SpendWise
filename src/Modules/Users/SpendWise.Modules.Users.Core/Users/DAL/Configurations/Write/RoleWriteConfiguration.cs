using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;

namespace SpendWise.Modules.Users.Core.Users.DAL.Configurations.Write;

internal class RoleWriteConfiguration : IEntityTypeConfiguration<Role>
{
    private const int NameMaxLength = 100;
    private const char Separator = ',';

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(q => q.Name);

        builder
            .Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder
            .Property(q => q.Permissions)
            .HasConversion(q => string.Join(Separator, q), q => q.Split(Separator, StringSplitOptions.None));

        builder
            .Property(x => x.Permissions).Metadata.SetValueComparer(
                new ValueComparer<IEnumerable<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, next) => HashCode.Combine(a, next.GetHashCode()))));
    }
}