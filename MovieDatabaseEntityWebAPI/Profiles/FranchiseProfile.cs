using AutoMapper;
using MovieDatabaseEntityWebAPI.Models.DTO.Character;
using MovieDatabaseEntityWebAPI.Models.DTO.Franchise;
using MovieDatabaseEntityWebAPI.Models.DTO.Movie;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<FranchisePostDto, Franchise>();
            CreateMap<FranchisePutDto, Franchise>();
            CreateMap<Franchise, FranchiseDto>()
            .ForMember(fdto => fdto.Movies, opt => opt
            .MapFrom(f => f.Movies.Select(m=>m.Id).ToArray()))
            .ReverseMap();
        }
    }
}
