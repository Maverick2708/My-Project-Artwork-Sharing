using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    public partial class AddIdentityDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genre_Artwork = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "MoMo",
                columns: table => new
                {
                    MomoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone_MoMo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name_MoMo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoMo", x => x.MomoId);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateUserRe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    ArtworkPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content_Artwork = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price_Artwork = table.Column<double>(type: "float", nullable: true),
                    Date_Post = table.Column<DateTime>(type: "date", nullable: true),
                    Picture_Artwork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quanity = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Post__551479477F27FEF3", x => x.ArtworkPId);
                    table.ForeignKey(
                        name: "FK__Post__Genre_Id__3A81B327",
                        column: x => x.Genre_ID,
                        principalTable: "Genre",
                        principalColumn: "GenreId");
                    table.ForeignKey(
                        name: "FK__Post__User_ID__3B75D760",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Person_UserId",
                        column: x => x.UserId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    FollowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserID_Follow = table.Column<int>(type: "int", nullable: true),
                    Date_Follow = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => x.FollowId);
                    table.ForeignKey(
                        name: "FK__Follow__User_ID__4BAC3F29",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Content_Noti = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date_Noti = table.Column<DateTime>(type: "date", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK__Notificat__User___5165187F",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    BillOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Total_Bill = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BillOrde__4CCEEDA28878898D", x => x.BillOrderId);
                    table.ForeignKey(
                        name: "FK__BillOrder__User___44FF419A",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK__Request__User_ID__4E88ABD4",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content_Comment = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date_Sub = table.Column<DateTime>(type: "date", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ArtworkP_ID = table.Column<int>(type: "int", nullable: true),
                    IsLikePost = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK__Comment__Artwork__5535A963",
                        column: x => x.ArtworkP_ID,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkPId");
                    table.ForeignKey(
                        name: "FK__Comment__User_ID__5441852A",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RateStar",
                columns: table => new
                {
                    RateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ArtworkP_ID = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RateStar__30BADA523D8F2DF0", x => x.RateId);
                    table.ForeignKey(
                        name: "FK__RateStar__Artwor__7C4F7684",
                        column: x => x.ArtworkP_ID,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkPId");
                    table.ForeignKey(
                        name: "FK__RateStar__User_I__7B5B524B",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtworkP_ID = table.Column<int>(type: "int", nullable: true),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK__Report__ArtworkP__05D8E0BE",
                        column: x => x.ArtworkP_ID,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkPId");
                    table.ForeignKey(
                        name: "FK__Report__User_ID__06CD04F7",
                        column: x => x.User_ID,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Order = table.Column<DateTime>(type: "date", nullable: true),
                    BillOrder_ID = table.Column<int>(type: "int", nullable: true),
                    Price_Order = table.Column<double>(type: "float", nullable: true),
                    ArtworkP_ID = table.Column<int>(type: "int", nullable: true),
                    Quanity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Artwo__48CFD27E",
                        column: x => x.ArtworkP_ID,
                        principalTable: "Artwork",
                        principalColumn: "ArtworkPId");
                    table.ForeignKey(
                        name: "FK__OrderDeta__BillO__47DBAE45",
                        column: x => x.BillOrder_ID,
                        principalTable: "Order",
                        principalColumn: "BillOrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_Genre_ID",
                table: "Artwork",
                column: "Genre_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_User_ID",
                table: "Artwork",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArtworkP_ID",
                table: "Comment",
                column: "ArtworkP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_User_ID",
                table: "Comment",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_User_ID",
                table: "Follow",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_User_ID",
                table: "Notification",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_User_ID",
                table: "Order",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ArtworkP_ID",
                table: "OrderDetail",
                column: "ArtworkP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_BillOrder_ID",
                table: "OrderDetail",
                column: "BillOrder_ID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Person",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Person",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RateStar_ArtworkP_ID",
                table: "RateStar",
                column: "ArtworkP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RateStar_User_ID",
                table: "RateStar",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ArtworkP_ID",
                table: "Report",
                column: "ArtworkP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_User_ID",
                table: "Report",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_User_ID",
                table: "Request",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "MoMo");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "RateStar");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Artwork");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
