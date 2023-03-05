using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MovieDatabaseEntityWebAPI.Models
{
    public class MoviesDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Franchise> Franchises { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = N-NO-01-01-5225\\SQLEXPRESS; Initial Catalog = MoviesDB; Integrated Security = True; Trust Server Certificate = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Characters

            modelBuilder.Entity<Character>().HasData(
                new Character() { Id = 1, Name = "Keanu Reeves", Alias = "Neo", Gender = "Male", Picture = "https://www.indiewire.com/wp-content/uploads/2017/07/the-matrix-revolutions.jpg" },
                new Character() { Id = 2, Name = "Carrie-Anne Moss", Alias = "Trinity", Gender = "Female", Picture = "https://sm.ign.com/t/ign_nl/screenshot/b/bcarrie-an/bcarrie-anne-moss-as-trinitybbrbrcarrie-anne-moss-will-also_rhwq.1080.jpg" },
                new Character() { Id = 3, Name = "Christian Bale", Alias = "Batman", Gender = "Male", Picture = "https://variety.com/wp-content/uploads/2022/06/Screen-Shot-2022-06-27-at-9.56.12-AM.png" }
                );

            // Students

            modelBuilder.Entity<Movie>().HasData(
                new Movie() { Id = 1, Title = "The Matrix", Genre = "Action, Science fiction", ReleaseYear = "1999", Director = "Wachowski", Picture = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg", Trailer = "https://www.youtube.com/watch?v=vKQi3bBA1y8", FranchiseId = 1 },
                new Movie() { Id = 2, Title = "The Dark Knight", Genre = "Action, Superhero", ReleaseYear = "2008", Director = "Christopher Nolan", Picture = "https://m.media-amazon.com/images/M/MV5BODE0MzZhZTgtYzkwYi00YmI5LThlZWYtOWRmNWE5ODk0NzMxXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg", Trailer = "https://www.youtube.com/watch?v=EXeTwQWrcwY", FranchiseId = 2 },
                new Movie() { Id = 3, Title = "The Matrix Reloaded", Genre = "Action, Science fiction", ReleaseYear = "2003", Director = "Wachowski", Picture = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg", Trailer = "https://www.youtube.com/watch?v=kYzz0FSgpSU", FranchiseId = 1 }
                );

            // Projects

            modelBuilder.Entity<Franchise>().HasData(
                new Franchise() { Id = 1, Name = "The Matrix", Description = "Movie series where the protagonist realizes that the world is just a stimulation and a virus in the form of a human is corrupting it" },
                new Franchise() { Id = 2, Name = "Batman", Description = "The protagonist's billionaire parents are murdered at a young age. He uses his inhertited money to buy armor and car to be used for fighting criminals" }
                );

            // Character Movies

            modelBuilder.Entity<Character>()
                .HasMany(std => std.Movies)
                .WithMany(sub => sub.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    l => l.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                    r => r.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                    je =>
                    {
                        je.HasKey("CharactersId", "MoviesId");
                        je.HasData(
                            new { CharactersId = 1, MoviesId = 1 },
                            new { CharactersId = 2, MoviesId = 1 },
                            new { CharactersId = 1, MoviesId = 3 },
                            new { CharactersId = 2, MoviesId = 3 },
                            new { CharactersId = 3, MoviesId = 2 });
                    }
                );

        }


    }
}
