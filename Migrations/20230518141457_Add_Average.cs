using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieMania.Migrations
{
    /// <inheritdoc />
    public partial class Add_Average : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovies_Genres_GenreId",
                table: "GenreMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovies_Movies_MovieId",
                table: "GenreMovies");

            migrationBuilder.AddColumn<float>(
                name: "averageRate",
                table: "Movies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "popularity",
                table: "Movies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovies_Genres_GenreId",
                table: "GenreMovies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovies_Movies_MovieId",
                table: "GenreMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovies_Genres_GenreId",
                table: "GenreMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovies_Movies_MovieId",
                table: "GenreMovies");

            migrationBuilder.DropColumn(
                name: "averageRate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "popularity",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovies_Genres_GenreId",
                table: "GenreMovies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovies_Movies_MovieId",
                table: "GenreMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
