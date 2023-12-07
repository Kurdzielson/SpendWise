using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Customers.Core.Customers.Domain.Entities;
using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.CreatedAt;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Email;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.FullName;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick;

namespace SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Write;

internal class CustomerWriteConfiguration : IEntityTypeConfiguration<Customer>
{
    private const int NickMaxLength = 50;
    private const int EmailMaxLength = 100;
    private const int FullNameMaxLength = 100;

    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(q => q.Id);

        builder.HasIndex(q => q.Nick)
            .IsUnique();

        builder.HasIndex(q => q.Email)
            .IsUnique();

        builder.Property<CustomerId>("Id")
            .HasConversion(q => q.Value, q => new CustomerId(q));

        builder.Property<Nick>("Nick")
            .HasMaxLength(NickMaxLength)
            .HasConversion(q => q.Value, q => new Nick(q));

        builder.Property<Email>("Email")
            .IsRequired()
            .HasMaxLength(EmailMaxLength)
            .HasConversion(q => q.Value, q => new Email(q));

        builder.Property<FullName>("FullName")
            .HasMaxLength(FullNameMaxLength)
            .HasConversion(q => q.Value, q => new FullName(q));

        builder.Property<Date>("CompletedAt")
            .HasConversion(q => q.Value, q => new Date(q));

        builder.Property<CreatedAt>("CreatedAt")
            .HasConversion(q => q.Value, q => new CreatedAt(q));

        builder.Property<CustomerState>("State")
            .HasConversion(q => q.Code, q => AvailableCustomerStates.GetState(q));

        builder.Property<Date>("VerifiedAt")
            .HasConversion(q => q.Value, q => new Date(q));
    }
}