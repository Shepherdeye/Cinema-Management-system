namespace Cinematic_Assets_Management.ModelsView
{
    public class ActorsData
    {
        public Actor? Actor { get; set; }
        public List<ActorMovies> ActorMovies { get; set; } = new();
    }
}
