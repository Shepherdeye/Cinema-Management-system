using System.ComponentModel.DataAnnotations;

namespace Cinematic_Assets_Management.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();

    }
}
