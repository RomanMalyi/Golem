using Golem.Data.PostgreSql.Models;
using Microsoft.EntityFrameworkCore;

namespace Golem.Data.PostgreSql
{
    public sealed class GolemContext : DbContext
    {
        public GolemContext(DbContextOptions<GolemContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Query> Queries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Queries)
                .WithOne(q => q.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
