using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(20)
            .HasConversion(
                name => name!.Value,
                value => new Name(value));

        builder.Property(c => c.Email)
            .HasMaxLength(25)
            .HasConversion(
                email => email!.Value,
                value => Email.Create(value));

        builder.Property(c => c.Phone)
            .HasMaxLength(10)
            .HasConversion(
                phone => phone!.Value,
                value => new Phone(value));

        builder.HasIndex(c => c.Email).IsUnique();
    }
}
