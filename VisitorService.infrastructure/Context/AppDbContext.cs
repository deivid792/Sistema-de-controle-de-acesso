using Microsoft.EntityFrameworkCore;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Shared;
using VisitorService.Domain.ValueObject;
using VisitorService.Domain.ValueObjects;
using VisitorService.Domain.Enums;

namespace VisitorService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<ValidationToken> ValidationTokens => Set<ValidationToken>();
        public DbSet<Visit> Visits => Set<Visit>();
        public DbSet<VisitHistory> VisitHistories => Set<VisitHistory>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<NotificationItem>();

            var userBuilder = modelBuilder.Entity<User>();

            // Name
            userBuilder.Property(u => u.Name)
                .HasConversion(
                    v => v.Value,
                    v => Name.FromDatabase(v)
                );

            // Email
            userBuilder.OwnsOne(u => u.Email, email =>
            {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(254)
                .IsRequired();
            });

            // PasswordHash
            userBuilder.Property(u => u.Password)
                .HasConversion(
                    v => v.Value,
                    v => Password.FromHash(v)
                );

            // Phone
            userBuilder.Property(u => u.Phone)
                .HasConversion(
                    v => v!.Value,
                    v => Phone.FromDatabase(v)
                );

            // CNPJ
            userBuilder.Property(u => u.Cnpj)
                .HasConversion(
                    v => v!.Value,
                    v => Cnpj.FromDatabase(v)
                );

            // ROLE
            var roleBuilder = modelBuilder.Entity<Role>();

            roleBuilder.Property(r => r.Name)
                .HasConversion(
                    v => (int)v.Value,
                    v => RoleName.FromDatabase((RoleType)v)
                )
                .HasColumnName("RoleType")
                .IsRequired();

            // USER ROLE
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}