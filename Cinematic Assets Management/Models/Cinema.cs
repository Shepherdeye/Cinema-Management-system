using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinematic_Assets_Management.Models
{
    //Name, Description, CinemaLogo, Address
    public class Cinema
    {
        [Key]
        public int Id { get; set; } 
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [MinLength(10)]
        public string Description { get; set; } = string.Empty;
        [MinLength(10)]

        public string CinemaLogo { get; set; } = string.Empty;
        [MinLength(10)]

        public string Address { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();


    }
}
