using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinematic_Assets_Management.Migrations
{
    /// <inheritdoc />
    public partial class add_data_categories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("insert into Categories (Name) values ('Action');insert into Categories (Name) values ('Comedy');insert into Categories (Name) values ('Drama');insert into Categories (Name) values ('Sci-Fi');insert into Categories (Name) values ('Horror');  ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Categories");
        }
    }
}
