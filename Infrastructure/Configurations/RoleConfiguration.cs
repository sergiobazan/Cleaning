using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(x => x.Id);

        builder
            .HasMany(r => r.Customers)
            .WithMany(c => c.Roles);

        builder.HasData(
        [
            new
            {
                Id = Guid.NewGuid(),
                Name = "Admin"
            },
            new {
                Id = Guid.NewGuid(),
                Name = "Customer"
            }
        ]);
    }
}
