namespace Cinematic_Assets_Management.ModelsView
{
    public class MovieFilter
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public MovieStatus? MovieStatus { get; set; }
        public int? Category { get; set; }
        public int? Cinema { get; set; }
    }
}
