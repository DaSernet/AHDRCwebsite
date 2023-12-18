using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AHDRCwebsite.Migrations
{
    public partial class confidential : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvailableForPublic",
                table: "Artworks",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
