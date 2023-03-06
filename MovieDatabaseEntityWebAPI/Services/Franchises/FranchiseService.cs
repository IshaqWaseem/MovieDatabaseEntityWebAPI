using Microsoft.EntityFrameworkCore;
using MovieDatabaseEntityWebAPI.Exceptions;
using MovieDatabaseEntityWebAPI.Models;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        private readonly MoviesDbContext _context;
        private readonly ILogger<FranchiseService> _logger;

        public FranchiseService(MoviesDbContext context, ILogger<FranchiseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Franchise entity)
        {
            await _context.Franchises.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            // Log and throw pattern
            if (franchise == null)
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new CharacterNotFoundException();
            }
            // We set our entities to have nullable relationships
            // so it removes the FKs when delete it.
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _context.Franchises.ToListAsync();
        }

        public async Task<Franchise> GetByIdAsync(int id)
        {
            // Log and throw error handling
            if (!await FranchiseExistsAsync(id))
            {
                _logger.LogError("Franchise not found with Id: " + id);
                throw new FranchiseNotFoundException();
            }
            // Want to include all related data for professor
            return await _context.Franchises
                .Where(f => f.Id == id)
                .Include(f => f.Movies)
                .FirstAsync();
        }

        public async Task UpdateAsync(Franchise entity)
        {
            // Log and throw pattern
            if (!await FranchiseExistsAsync(entity.Id))
            {
                _logger.LogError("Franchise not found with Id: " + entity.Id);
                throw new CharacterNotFoundException();
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        private async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _context.Franchises.AnyAsync(e => e.Id == id);
        }
    }
}
