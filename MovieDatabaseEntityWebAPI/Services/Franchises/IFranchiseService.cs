using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        Task UpdateMoviesAsync(int[] characterIds, int movieId);
        Task<IEnumerable<Movie>> GetFranchisesAsync(int franchiseId);

    }
}
