using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Users.Core.DAL.Configurations.Read.Model;
using SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read.Model;

namespace SpendWise.Modules.Users.Core.Users.DAL.Configurations.Read;

internal class UserReadConfiguration : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(q => q.Id);
    }
}