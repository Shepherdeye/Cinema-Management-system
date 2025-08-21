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
        [MaxLength(20)]
        [MinLength(5)]
        public string Name { get; set; } = string.Empty;
        [MinLength(5)]
        public string Description { get; set; } = string.Empty;

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
    }
}
