using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    public partial class UpdateArtworkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Artwork",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Artwork");
        }
    }
}
