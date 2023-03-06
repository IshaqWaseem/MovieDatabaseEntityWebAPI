namespace MovieDatabaseEntityWebAPI.Models.DTO.Character
{
    public class CharacterPostDto
    {
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Picture { get; set; } = null!;
    }
}
