using System.ComponentModel.DataAnnotations;

namespace Cinematic_Assets_Management.Models
{
    public class Images
    {
        [Key]
        public  int Id { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

    }
}
