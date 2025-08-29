using System.ComponentModel.DataAnnotations;

namespace Cinematic_Assets_Management.Models
{
    public enum MovieStatus
    {
        Available,
        Expired,
        Upcoming
    }
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [MinLength(5,ErrorMessage ="Name should be More than 5")]
        [MaxLength(20,ErrorMessage = "Name should be less than 20")]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MinLength(5)]
        public string Description { get; set; } = string.Empty;
        [Required,Range(0,double.MaxValue,ErrorMessage ="can't use negative values")]
        
        public decimal Price { get; set; }
       
        public string ImgUrl { get; set; } = string.Empty;

        public string TrailerUrl { get; set; } = string.Empty;


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MovieStatus MovieStatus { get; set; }

        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<ActorMovies> ActorMovies { get; set; } = new List<ActorMovies>();

        public ICollection<Images> Images { get; set; } = new List<Images>();
    }
}
