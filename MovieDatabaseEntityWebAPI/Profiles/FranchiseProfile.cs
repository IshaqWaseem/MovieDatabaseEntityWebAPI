using AutoMapper;
using MovieDatabaseEntityWebAPI.Models.DTO.Franchise;
using MovieDatabaseEntityWebAPI.Models.DTO.Movie;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDto>()
            .ForMember(fdto => fdto.Movies, opt => opt
            .MapFrom(f => f.Movies)).ReverseMap();
        }
    }
}
