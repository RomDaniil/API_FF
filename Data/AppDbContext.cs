using Microsoft.EntityFrameworkCore;
using Screenly_api.Models;

namespace Screenly_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Title> Titles => Set<Title>();
        public DbSet<Genre> Genres => Set<Genre>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Title>().ToTable("Titles");
            modelBuilder.Entity<Genre>().ToTable("Genres");

            modelBuilder.Entity<Title>().Property(t => t.Rating).HasPrecision(3, 1);
        }
    }
}