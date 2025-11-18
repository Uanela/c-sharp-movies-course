using Microsoft.EntityFrameworkCore;
using Backend.Entities;

namespace Backend.Data {
    public class MovieContext(DbContextOptions<MovieContext> options): DbContext(options) {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // seed
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Sci-Fi" },
                new Genre { Id = 3, Name = "Fantasy" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Romance" }
            );

            modelBuilder.Entity<Movie>().Property(m => m.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Movie>().Property(m => m.Name).HasMaxLength(100);

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Name = "Inception", GenreId = 1, Price = 12.99M, ReleaseDate = new DateOnly(2010, 1, 1) },
                new Movie { Id = 2, Name = "The Dark Knight", GenreId = 1, Price = 14.99M, ReleaseDate = new DateOnly(2008, 1, 1) },
                new Movie { Id = 3, Name = "Interstellar", GenreId = 2, Price = 10.99M, ReleaseDate = new DateOnly(2014, 1, 1) },
                new Movie { Id = 4, Name = "The Matrix", GenreId = 2, Price = 9.99M, ReleaseDate = new DateOnly(1999, 1, 1) },
                new Movie { Id = 5, Name = "The Lord of the Rings: The Fellowship of the Ring", GenreId = 3, Price = 11.99M, ReleaseDate = new DateOnly(2001, 1, 1) },
                new Movie { Id = 6, Name = "The Lord of the Rings: The Two Towers", GenreId = 3, Price = 11.99M, ReleaseDate = new DateOnly(2002, 1, 1) }
            );
        }
    }
}
