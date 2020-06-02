using Golem.Data.PostgreSql.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Golem.Data.PostgreSql
{
    public sealed class GolemContext : IdentityDbContext<AppUser>
    {
        public GolemContext(DbContextOptions<GolemContext> options)
            : base(options)
        {
        }

        public DbSet<User> AnalyticUsers { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Queries)
                .WithOne(q => q.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasMany(usr => usr.Sessions)
                .WithOne(sess => sess.User)
                .HasForeignKey(sess => sess.UserId);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<AppUser>(u => u.RefreshTokenId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
