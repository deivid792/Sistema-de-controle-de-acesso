using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using VisitorService.Domain.Entities;

namespace VisitorService.Infrastructure.Mappings;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.OwnsOne(r => r.Name, name =>
        {
            name.Property(n => n.Value)
            .HasColumnName("Name")
            .IsRequired();
        });

        builder.Property(r => r.Description)
        .HasColumnName("Description")
        .HasMaxLength(100)
        .IsRequired();
    }
}