using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    public partial class UpdateFieldOfOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "OrderDetail");
        }
    }
}
