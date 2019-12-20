using DotNetSurfer_Backend.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DotNetSurfer_Backend.Infrastructure.Entities
{
    public class DotNetSurferDbContext : DbContext
    {
        public DotNetSurferDbContext(DbContextOptions<DotNetSurferDbContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Feature> Features { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
