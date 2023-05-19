using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieMania.Migrations
{
    /// <inheritdoc />
    public partial class AddedTMDBId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TMDBId",
                table: "Producers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TMDBId",
                table: "Producers");
        }
    }
}
