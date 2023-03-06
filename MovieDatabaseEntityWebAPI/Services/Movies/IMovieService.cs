using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Services.Movies
{
    public interface IMovieService : ICrudService<Movie,int>
    {
        Task UpdateCharactersAsync(int[] characterIds, int movieId);
    }
}
