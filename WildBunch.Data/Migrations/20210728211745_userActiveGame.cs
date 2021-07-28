using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBunch.Data.Migrations
{
    public partial class userActiveGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActiveGameId",
                table: "tblUser",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveGameId",
                table: "tblUser");
        }
    }
}
