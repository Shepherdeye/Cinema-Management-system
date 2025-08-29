namespace Cinematic_Assets_Management.ModelsView
{
    public class MovieWithData
    {
        public List<Movie> Movies { get; set; } = new();
        public List<Cinema> Cinemas { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Movie> Slider { get; set; } = new();
        public Movie? Movie { get; set; }
        public List<Actor>? Actors { get; set; }
        public List<int> ActorsIds { get; set; }

    }
}
