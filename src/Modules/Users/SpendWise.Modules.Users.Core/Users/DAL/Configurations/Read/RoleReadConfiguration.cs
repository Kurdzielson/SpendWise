using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Users.Core.DAL.Configurations.Read.Model;

namespace SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read;

internal class RoleReadConfiguration : IEntityTypeConfiguration<RoleReadModel>
{
    private const char Separator = ',';

    public void Configure(EntityTypeBuilder<RoleReadModel> builder)
    {
        builder.ToTable("Roles");

        builder
            .HasKey(q => q.Name);

        builder
            .Property(q => q.Permissions)
            .HasConversion(q => string.Join(Separator, q), q => q.Split(Separator, StringSplitOptions.None));

        builder
            .Property(q => q.Permissions).Metadata.SetValueComparer(
                new ValueComparer<IEnumerable<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, next) => HashCode.Combine(a, next.GetHashCode()))));
    }
}