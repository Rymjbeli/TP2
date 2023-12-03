using Microsoft.EntityFrameworkCore;

namespace Tp2.Models
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<Movie>? movies { get; set; }
        public DbSet<Genre> genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId);
        }

    }
}