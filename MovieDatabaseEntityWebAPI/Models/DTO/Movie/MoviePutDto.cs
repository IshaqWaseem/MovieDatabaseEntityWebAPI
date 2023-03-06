namespace MovieDatabaseEntityWebAPI.Models.DTO.Movie
{
    public class MoviePutDto
    {
        public int Id { get; set; }
        public string Picture { get; set; } = null!;
        public string Trailer { get; set; } = null!;
    }
}
