using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseEntityWebAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60)]
        [Required]
        public string Title { get; set; } = null!;
        [MaxLength(60)]
        [Required]
        public string Genre { get; set; } = null!;
        [MaxLength(4)]
        [Required]
        public string ReleaseYear { get; set; } = null!;
        [MaxLength(60)]
        [Required]
        public string Director { get; set; } = null!;
        [MaxLength(256)]
        public string? Picture { get; set; }
        [MaxLength(256)]
        public string? Trailer { get; set; }
        //navigation
        public int? FranchiseId { get; set; }
        public virtual ICollection<Character>? Characters { get; set; }
    }
}
