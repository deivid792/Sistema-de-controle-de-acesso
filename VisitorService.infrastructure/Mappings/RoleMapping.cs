using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;

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

            name.HasData(
            new { RoleId = Guid.Parse("72579b2a-795a-4700-9118-09549d44c77c"), Value = RoleType.Visitor },
            new { RoleId = Guid.Parse("f399f57d-e692-411a-853f-42a9693952f4"), Value = RoleType.Manager },
            new { RoleId = Guid.Parse("1826500d-587b-4009-8d7b-422838183186"), Value = RoleType.Security });
        });

        builder.Property(r => r.Description)
        .HasColumnName("Description")
        .HasMaxLength(300)
        .IsRequired();

        builder.HasData(
            new
            {
                Id = Guid.Parse("72579b2a-795a-4700-9118-09549d44c77c"),
                Description = "Perfil com permissões limitadas, focado na visualização de dados e gerenciamento do próprio cadastro."
            },
            new
            {
                Id = Guid.Parse("f399f57d-e692-411a-853f-42a9693952f4"),
                Description = "Acesso total ao sistema, com poderes para gerenciar usuários, configurar roles, extrair relatórios e auditar logs"
            },
            new
            {
                Id = Guid.Parse("1826500d-587b-4009-8d7b-422838183186"),
                Description = "Responsável pelo controle de fluxo, execução de check-in, check-out e monitoramento de acessos em tempo real."
            }
        );
    }
}