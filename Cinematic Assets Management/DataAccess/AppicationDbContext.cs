using Microsoft.EntityFrameworkCore;

namespace Cinematic_Assets_Management.DataAccess
{
    public class AppicationDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ActorMovies> ActorMovies { get; set; }
        public DbSet<Images> Images { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Cinema;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<ActorMovies>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => new {e.MovieId,e.ActorId }).IsUnique();
                
                entity.HasOne(e => e.Movie)
                .WithMany(e => e.ActorMovies)
                .HasForeignKey(e => e.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Actor)
                .WithMany(e => e.ActorMovies)
                .HasForeignKey(e => e.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            });






        }
    }
}
