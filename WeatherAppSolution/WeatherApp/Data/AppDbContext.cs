using Microsoft.EntityFrameworkCore;
using WeatherApp.Entities;

namespace WeatherApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<WeatherHistory>()
                .HasOne(w => w.User)
                .WithMany(u => u.WeatherHistories)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeatherHistory>()
                .HasIndex(w => w.UserId);

            modelBuilder.Entity<WeatherHistory>()
                .HasIndex(w => w.RequestedAt);
        }


        public DbSet<WeatherHistory> WeatherHistories => Set<WeatherHistory>();
        public DbSet<User> Users => Set<User>();
    }
}
