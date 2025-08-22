using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinematic_Assets_Management.Migrations
{
    /// <inheritdoc />
    public partial class set_aactorMovie_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into ActorMovies (ActorId, MovieId) values (1, 1);insert into ActorMovies (ActorId, MovieId) values (2,2);insert into ActorMovies (ActorId, MovieId) values (3, 3);insert into ActorMovies (ActorId, MovieId) values (4, 4);insert into ActorMovies (ActorId, MovieId) values (5, 5);insert into ActorMovies (ActorId, MovieId) values (6,6);insert into ActorMovies (ActorId, MovieId) values (1, 6);insert into ActorMovies (ActorId, MovieId) values (2, 3);insert into ActorMovies (ActorId, MovieId) values (3, 4);insert into ActorMovies (ActorId, MovieId) values (4,5);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE ActorMovies");
        }
    }
}
