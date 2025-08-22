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
            migrationBuilder.Sql("insert into ActorMovies (ActorId, MovieId) values (49, 2);insert into ActorMovies (ActorId, MovieId) values (46, 4);insert into ActorMovies (ActorId, MovieId) values (41, 2);insert into ActorMovies (ActorId, MovieId) values (28, 5);insert into ActorMovies (ActorId, MovieId) values (37, 1);insert into ActorMovies (ActorId, MovieId) values (23, 2);insert into ActorMovies (ActorId, MovieId) values (48, 4);insert into ActorMovies (ActorId, MovieId) values (33, 4);insert into ActorMovies (ActorId, MovieId) values (25, 3);insert into ActorMovies (ActorId, MovieId) values (17, 6);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE ActorMovies");
        }
    }
}
