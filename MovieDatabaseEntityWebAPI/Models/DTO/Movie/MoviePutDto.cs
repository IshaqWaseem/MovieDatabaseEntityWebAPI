namespace MovieDatabaseEntityWebAPI.Models.DTO.Movie
{
    public class MoviePutDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string ReleaseYear { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string? Picture { get; set; }

        public string? Trailer { get; set; }
        public int? FranchiseId { get; set; }
    }
}
