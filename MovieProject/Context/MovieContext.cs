using Microsoft.EntityFrameworkCore;
using MovieProject.Models;

namespace MovieProject.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Film ile Tür ilişkisini tanımlama
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)  // Film ile Türler
                .WithMany(g => g.Movies)  // Tür ile Filmler
                .UsingEntity(j => j.ToTable("MovieGenres")); // İlişki tablosu ismi
        }
    }
}
