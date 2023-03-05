using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseEntityWebAPI.Models
{
    public class Franchise
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60)]
        [Required]
        public string Name { get; set; } = null!;
        [MaxLength(150)]
        public string? Description { get; set; }
        //Navigation
        public virtual ICollection<Movie>? Movies { get; set; }
    }
}
