using Microsoft.AspNetCore.Mvc;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Services.Movies
{
    public interface IMovieService : ICrudService<Movie,int>
    {
        Task UpdateCharactersAsync(int[] characterIds, int movieId);
        Task<IEnumerable<Character>> GetCharactersAsync(int movieId);
    }
}
