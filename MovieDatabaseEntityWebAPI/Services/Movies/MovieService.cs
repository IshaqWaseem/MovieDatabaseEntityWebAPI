using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseEntityWebAPI.Exceptions;
using MovieDatabaseEntityWebAPI.Models;
using MovieDatabaseEntityWebAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
namespace MovieDatabaseEntityWebAPI.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly MoviesDbContext _context;
        private readonly ILogger<MovieService> _logger;

        public MovieService(MoviesDbContext context, ILogger<MovieService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Movie obj)
        {
            await _context.Movies.AddAsync(obj);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteByIdAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            // Log and throw pattern
            if (movie == null)
            {
                _logger.LogError("Movie not found with Id: " + id);
                throw new MovieNotFoundException();
            }
            // We set our entities to have nullable relationships
            // so it removes the FKs when delete it.
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _context.Movies.Include(m => m.Characters).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!await MovieExistsAsync(id))
            {
                _logger.LogError("Movie not found with Id: " + id);
                throw new MovieNotFoundException();
            }
            return await _context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Characters)
                .FirstAsync();
        }

        public async Task UpdateAsync(Movie entity)
        {
            // Log and throw pattern
            if (!await MovieExistsAsync(entity.Id))
            {
                _logger.LogError("Movie not found with Id: " + entity.Id);
                throw new CharacterNotFoundException();
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        public async Task UpdateCharactersAsync(int[] characterIds, int movieId)
        {
            // Log and throw pattern
            if (!await MovieExistsAsync(movieId))
            {
                _logger.LogError("Movie not found with Id: " + movieId);
                throw new MovieNotFoundException();
            }
           
            List<Character> characters = characterIds
                .ToList()
                .Select(sid => _context.Characters
                .Where(s => s.Id == sid).First())
                .ToList();

            Movie movie = await _context.Movies
                .Where(p => p.Id == movieId)
                .FirstAsync();

            movie.Characters = characters;
            _context.Entry(movie).State = EntityState.Modified;
            // Save all the changes
            await _context.SaveChangesAsync();
        }
        public  async Task<IEnumerable<Character>> GetCharactersAsync(int movieId)

        {
            // Log and throw error handling
            if (!await MovieExistsAsync(movieId))
            {
                _logger.LogError("Movie not found with Id: " + movieId);
                throw new MovieNotFoundException();
            }
            var movie = await _context.Movies
                .Include(m=>m.Characters)
                .SingleOrDefaultAsync(m=>m.Id== movieId);

            
            return movie?.Characters ?? Enumerable.Empty<Character>();
          


        }

        private async Task<bool> MovieExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(e => e.Id == id);
        }

        
    }
}
