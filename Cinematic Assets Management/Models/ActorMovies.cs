using System.ComponentModel.DataAnnotations.Schema;

namespace Cinematic_Assets_Management.Models
{
    //ActorsId, MoviesId
    public class ActorMovies
    {
        public int Id { get; set; }  
        public int ActorId { get; set; }
        public Actor Actor { get; set; }  
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}
