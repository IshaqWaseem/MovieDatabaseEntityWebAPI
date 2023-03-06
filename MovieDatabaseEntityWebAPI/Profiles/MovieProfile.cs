using AutoMapper;
using MovieDatabaseEntityWebAPI.Models.DTO.Franchise;
using MovieDatabaseEntityWebAPI.Models.DTO.Movie;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() {
            CreateMap<MoviePutDto, Movie>();
            CreateMap<MoviePostDto, Movie>();
            CreateMap<Movie, MovieDto>()
            .ForMember(mdto => mdto.Characters, opt => opt
            .MapFrom(m => m.Characters.Select(m => m.Id).ToArray()));

        }
    }
}
