using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Users.Core.Users.Domain.Entities;
using SpendWise.Modules.Users.Core.Users.Domain.ValueObjects.State;
using SpendWise.Shared.Abstraction.Kernel.Types.UserId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Email;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Password;

namespace SpendWise.Modules.Users.Core.Users.DAL.Configurations.Write;

internal class UserWriteConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(q => q.Id);
        
        builder
            .HasIndex(q => q.Email)
            .IsUnique();

        builder
            .Property<UserId>("Id")
            .HasConversion(q => q.Value, q => new UserId(q));
        
        builder
            .Property<Email>("Email")
            .IsRequired()
            .HasConversion(q => q.Value, q => new Email(q));

        builder
            .Property<Password>("Password")
            .IsRequired()
            .HasConversion(q => q.Value, q => new Password(q));

        builder
            .Property<Date>("CreatedAt")
            .HasConversion(q => q.Value, q => new Date(q));

        builder
            .Property<UserState>("State")
            .HasConversion(q => q.Code, q => AvailableUserStates.GetState(q));
    }
}