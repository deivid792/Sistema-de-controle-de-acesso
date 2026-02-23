using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitorService.Domain.Entities;

namespace VisitorService.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.Value)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
            });
        
        builder.OwnsOne(u => u.Email, email =>
            {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(254)
                .IsRequired();
            });

        builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Value)
                .HasColumnName("Password")
                .HasMaxLength(100)
                .IsRequired();
            });

        builder.OwnsOne(u => u.Phone, phone =>
            {
                phone.Property(p => p.Value)
                .HasColumnName("Phone")
                .HasMaxLength(20)
                .IsRequired(false);
            });

        builder.OwnsOne(u => u.Cnpj, cnpj =>
            {
                cnpj.Property(c => c.Value)
                .HasColumnName("Cnpj")
                .HasMaxLength(50)
                .IsRequired(false);
            });
        
        builder.Property(u => u.CreatedByUserId)
        .IsRequired(false);

        builder.Property(u => u.CreatedByUserName)
        .HasMaxLength(50)
        .IsRequired(false);

        builder.HasMany(u => u.Roles)
        .WithMany(r => r.Users)
        .UsingEntity(i =>
        {
            i.ToTable("UserRoles");
            i.Property("RolesId").HasColumnName("RoleId");
            i.Property("UsersId").HasColumnName("UserId");
        });
    } 


}