using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITICommunity.Migrations
{
    public partial class AddApprovementDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovementDate",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovementDate",
                table: "AspNetUsers");
        }
    }
}
