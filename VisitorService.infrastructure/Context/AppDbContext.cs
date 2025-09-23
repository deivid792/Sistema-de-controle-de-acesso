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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        }
    }
}
