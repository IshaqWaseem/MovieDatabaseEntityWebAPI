using AutoMapper;
using MovieDatabaseEntityWebAPI.Models.DTO.Character;
using MovieDatabaseEntityWebAPI.Models.DTO.Movie;
using MovieDatabaseEntityWebAPI.Models.Entities;

namespace MovieDatabaseEntityWebAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()

        {
            CreateMap<CharacterPostDto, Character>();
            CreateMap<CharacterPutDto, Character>();

            CreateMap<Character, CharacterDto>()
                .ForMember(cdto => cdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(c => c.Id).ToArray()));


        }
    }
}
