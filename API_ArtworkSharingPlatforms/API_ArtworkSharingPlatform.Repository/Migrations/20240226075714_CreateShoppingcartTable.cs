using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    public partial class CreateShoppingcartTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtworkP_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quanity = table.Column<int>(type: "int", nullable: true),
                    Price_Artwork = table.Column<double>(type: "float", nullable: true),
                    Picture_Artwork = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.ShoppingCartId);
                    table.ForeignKey(
                        name: "FK__ShoppingCart__ArtworkP__08D8E0BE",
                        column: x => x.ArtworkP_ID,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkPId");
                    table.ForeignKey(
                        name: "FK__ShoppingCart__User_ID__09CD04F7",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ArtworkP_ID",
                table: "ShoppingCart",
                column: "ArtworkP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_User_ID",
                table: "ShoppingCart",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCart");
        }
    }
}
