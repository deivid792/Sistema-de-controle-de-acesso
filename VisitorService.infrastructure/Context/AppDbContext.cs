using Microsoft.EntityFrameworkCore;
using VisitorService.Domain.Entities;

namespace VisitorService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;
        public DbSet<ValidationToken> ValidationTokens { get; set; } = default!;
        public DbSet<Visit> Visits { get; set; } = default!;
        public DbSet<VisitHistory> VisitHistories { get; set; } = default!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userBuilder = modelBuilder.Entity<User>();

            userBuilder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            userBuilder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Value)
                    .HasColumnName("PasswordHash")
                    .IsRequired();
            });

            userBuilder.OwnsOne(u => u.Phone, phone =>
            {
                phone.Property(p => p.Value)
                    .HasColumnName("Phone")
                    .HasMaxLength(20);
            }).Navigation(p => p).IsRequired(false);;

            userBuilder.OwnsOne(u => u.Cnpj, cnpj =>
            {
                cnpj.Property(c => c.Value)
                    .HasColumnName("Cnpj")
                    .HasMaxLength(18); 
            }).Navigation(p => p).IsRequired(false);;

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

            modelBuilder.Entity<ValidationToken>()
                .HasOne(vt => vt.User)
                .WithMany(u => u.ValidationTokens)
                .HasForeignKey(vt => vt.UserId);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.User)
                .WithMany(u => u.Visits)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<VisitHistory>()
                .HasOne(vh => vh.Visit)
                .WithMany(v => v.VisitHistories)
                .HasForeignKey(vh => vh.VisitId);

                modelBuilder.Entity<RefreshToken>()
    .HasOne(rt => rt.User)
    .WithMany(u => u.RefreshTokens)
    .HasForeignKey(rt => rt.UserId);

        }
    }
}
