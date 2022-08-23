using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AHDRCwebsite.Migrations
{
    public partial class imagedetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Copyright",
                table: "ArtworkImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Courtesy",
                table: "ArtworkImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSize",
                table: "ArtworkImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photographer",
                table: "ArtworkImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copyright",
                table: "ArtworkImages");

            migrationBuilder.DropColumn(
                name: "Courtesy",
                table: "ArtworkImages");

            migrationBuilder.DropColumn(
                name: "ImageSize",
                table: "ArtworkImages");

            migrationBuilder.DropColumn(
                name: "Photographer",
                table: "ArtworkImages");
        }
    }
}
