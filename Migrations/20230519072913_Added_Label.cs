using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieMania.Migrations
{
    /// <inheritdoc />
    public partial class Added_Label : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Rates",
                newName: "Rate");

            migrationBuilder.AddColumn<float>(
                name: "Label",
                table: "Rates",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "Rates");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Rates",
                newName: "Score");
        }
    }
}
