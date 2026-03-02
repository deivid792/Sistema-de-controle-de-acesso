using Microsoft.EntityFrameworkCore;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Shared;
using VisitorService.Infrastructure.Mappings;

namespace VisitorService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Visit> Visits => Set<Visit>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveMaxLength(150);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<NotificationItem>();
            modelBuilder.Ignore<Notifiable>();

            modelBuilder.ApplyConfiguration(new UserMapping());

            modelBuilder.ApplyConfiguration(new RoleMapping());

            modelBuilder.ApplyConfiguration(new VisitMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}