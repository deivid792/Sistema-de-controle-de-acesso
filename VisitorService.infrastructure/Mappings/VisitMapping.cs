using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitorService.Domain.Entities;

namespace VisitorService.Infrastructure.Mappings;

public class VisitMapping : IEntityTypeConfiguration<Visit>
{
    public void Configure( EntityTypeBuilder<Visit> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).ValueGeneratedNever();

        builder.Property(v => v.Date)
        .HasColumnType("DATE")
        .IsRequired();

        builder.Property(v => v.Time)
        .HasColumnType("TIME")
        .IsRequired();

        builder.Property(v => v.Reason)
        .HasColumnName("Rearson")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(v => v.Category)
        .HasColumnName("Category")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(v => v.Status)
        .HasColumnName("Status")
        .HasMaxLength(20)
        .IsRequired();

        builder.Property(v => v.CheckIn)
        .HasColumnType("DATETIME2(0)")
        .IsRequired();

        builder.Property(v => v.CheckOut)
        .HasColumnType("DATETIME2(0)")
        .IsRequired();

        builder.ToTable("Visits", b => b.IsTemporal());
    }
}
