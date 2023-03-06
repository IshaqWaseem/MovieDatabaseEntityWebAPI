namespace MovieDatabaseEntityWebAPI.Models.DTO.Character
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Alias { get; set; }
        public string? Gender { get; set; }
        public string? Picture { get; set; }
        public List<int>? Movies { get; set; }
    }
}
