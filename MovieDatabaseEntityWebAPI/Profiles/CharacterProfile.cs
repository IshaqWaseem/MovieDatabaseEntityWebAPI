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

            CreateMap<Character, CharacterDto>();
            
        }
    }
}
