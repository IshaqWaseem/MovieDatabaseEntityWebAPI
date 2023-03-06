using AutoMapper;
using MovieDatabaseEntityWebAPI.Models.DTO.Movie;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile() { 
            CreateMap<Movie, MovieDto>()
                .ForMember(mdto=>mdto.Franchise, opt=>opt
                .MapFrom(m=>m.FranchiseId))
                .ReverseMap();
        }
    }
}
