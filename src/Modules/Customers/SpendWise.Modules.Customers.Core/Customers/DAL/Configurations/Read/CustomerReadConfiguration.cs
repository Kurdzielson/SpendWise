using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read.Model;

namespace SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read;

internal class CustomerReadConfiguration : IEntityTypeConfiguration<CustomerReadModel>
{
    public void Configure(EntityTypeBuilder<CustomerReadModel> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(q => q.Id);
    }
}