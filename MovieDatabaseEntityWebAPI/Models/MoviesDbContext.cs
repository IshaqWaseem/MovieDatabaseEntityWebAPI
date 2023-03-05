using Microsoft.EntityFrameworkCore;

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
    }
}
