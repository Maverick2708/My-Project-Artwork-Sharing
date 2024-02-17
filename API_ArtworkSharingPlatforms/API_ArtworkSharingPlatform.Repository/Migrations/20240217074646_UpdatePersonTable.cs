using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    public partial class UpdatePersonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerifiedPage",
                table: "Person",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerifiedPage",
                table: "Person");
        }
    }
}
