﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_ArtworkSharingPlatform.Repository.Migrations
{
    public partial class UpdateNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdReceive",
                table: "Notification",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdReceive",
                table: "Notification");
        }
    }
}
