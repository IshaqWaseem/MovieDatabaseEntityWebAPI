using System.ComponentModel.DataAnnotations;

namespace MovieDatabaseEntityWebAPI.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(60)]
        [Required]
        public string Name { get; set; } = null!;
        [MaxLength(40)]
        public string? Alias { get; set; }
        [MaxLength(30)]
        public string? Gender { get; set; }
        [MaxLength(256)]
        public string? Picture { get; set; }
        //navigation
        public virtual ICollection<Movie>? Movies { get; set; }
        

    }
}
