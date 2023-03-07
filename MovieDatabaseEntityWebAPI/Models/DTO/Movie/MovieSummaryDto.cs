namespace MovieDatabaseEntityWebAPI.Models.DTO.Movie
{
    public class MovieSummaryDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ReleaseYear { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string Picture { get; set; } = null!; 

        public string Trailer { get; set; } = null!;
    }
}
