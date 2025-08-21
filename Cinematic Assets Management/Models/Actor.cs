using System.ComponentModel.DataAnnotations;

namespace Cinematic_Assets_Management.Models
{


    //FirstName, LastName, Bio, ProfilePicture, News
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }=string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string News { get; set; } = string.Empty;
        public ICollection<ActorMovies> ActorMovies { get; set; } = new List<ActorMovies>();
    }
}
