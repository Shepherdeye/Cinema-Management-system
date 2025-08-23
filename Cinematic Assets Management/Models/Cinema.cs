using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinematic_Assets_Management.Models
{
    //Name, Description, CinemaLogo, Address
    public class Cinema
    {
        [Key]
        public int Id { get; set; } 
   
        public string Name { get; set; } = string.Empty;

 
        public string Description { get; set; } = string.Empty;

        public string CinemaLogo { get; set; } = string.Empty;
        

        public string Address { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();


    }
}
