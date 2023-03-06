namespace MovieDatabaseEntityWebAPI.Models.DTO.Movie
{
    public class MoviePostDto
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ReleaseYear { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Trailer { get; set; } = null!;
        public int FranchiseId { get; set; }

    }
}
