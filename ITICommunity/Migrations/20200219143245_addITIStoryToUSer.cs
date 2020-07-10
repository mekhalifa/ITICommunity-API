using Microsoft.EntityFrameworkCore.Migrations;

namespace ITICommunity.Migrations
{
    public partial class addITIStoryToUSer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ITIStory",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ITIStory",
                table: "AspNetUsers");
        }
    }
}
