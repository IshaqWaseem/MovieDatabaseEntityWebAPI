using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseEntityWebAPI.Models.Entities
{
    public class Franchise
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60)]
        [Required]
        public string Name { get; set; } = null!;
        [MaxLength(256)]
        public string? Description { get; set; }
        //Navigation
        public virtual ICollection<Movie>? Movies { get; set; }
    }
}
