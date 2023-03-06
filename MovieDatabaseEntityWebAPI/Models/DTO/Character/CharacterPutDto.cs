namespace MovieDatabaseEntityWebAPI.Models.DTO.Character
{
    public class CharacterPutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Alias { get; set; } = null!;
        public string Picture { get; set; } = null!;
    }
}
