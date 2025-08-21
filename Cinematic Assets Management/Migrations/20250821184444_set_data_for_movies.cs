using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinematic_Assets_Management.Migrations
{
    /// <inheritdoc />
    public partial class set_data_for_movies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Movies (Name, Description, Price, ImgUrl, TrailerUrl, StartDate, EndDate, MovieStatus, CinemaId, CategoryId)VALUES('Eternal Horizons', 'Description for movie one', 10.00, 'movie1.png', 'https://youtube.com/trailer1', '2025-08-20', '2025-09-05', 0, 1, 1),('Shadow of the Dawn', 'Description for movie two', 11.50, 'movie2.png', 'https://youtube.com/trailer2', '2025-08-21', '2025-09-06', 2, 1, 1),('Crimson Skies', 'Description for movie three', 9.75, 'movie3.png', 'https://youtube.com/trailer3', '2025-08-22', '2025-09-07', 0, 1, 1),('Whispers in the Dark', 'Description for movie four', 12.00, 'movie4.png', 'https://youtube.com/trailer4', '2025-08-23', '2025-09-08', 1, 1, 1),('The Last Odyssey', 'Description for movie five', 8.50, 'movie5.png', 'https://youtube.com/trailer5', '2025-08-24', '2025-09-09', 2, 1, 1),('Neon Dreams', 'Description for movie six', 13.00, 'movie6.png', 'https://youtube.com/trailer6', '2025-08-25', '2025-09-10', 0, 1, 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Movies");
        }
    }
}
