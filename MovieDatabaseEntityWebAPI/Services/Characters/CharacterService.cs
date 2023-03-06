using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieDatabaseEntityWebAPI.Exceptions;
using MovieDatabaseEntityWebAPI.Models;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly MoviesDbContext _context;
        private readonly ILogger<CharacterService> _logger;

        public CharacterService(MoviesDbContext context, ILogger<CharacterService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Character obj)
        {
            await _context.Characters.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            // Log and throw pattern
            if (character == null)
            {
                _logger.LogError("Character not found with Id: " + id);
                throw new CharacterNotFoundException();
            }
            // We set our entities to have nullable relationships
            // so it removes the FKs when delete it.
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<Character>> GetAllAsync()
        {
            return await _context.Characters.ToListAsync();
        }


        public Task<Character> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Character entity)
        {
            // Log and throw pattern
            if (!await CharacterExistsAsync(entity.Id))
            {
                _logger.LogError("Character not found with Id: " + entity.Id);
                throw new CharacterNotFoundException();
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        private async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(e => e.Id == id);
        }

    }
}
