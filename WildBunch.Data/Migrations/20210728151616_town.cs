using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WildBunch.Data.Migrations
{
    public partial class town : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "tblGame",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblTown",
                columns: table => new
                {
                    TownId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Stranger = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTown", x => x.TownId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "tblTown",
                columns: new[] { "TownId", "Name", "Stranger" },
                values: new object[,]
                {
                    { 1, "Dry Gulch", 0 },
                    { 2, "Dodge City", 0 },
                    { 3, "Nugget City", 0 },
                    { 4, "BulletVille", 0 },
                    { 5, "Deadman's Creek", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblGame_TownId",
                table: "tblGame",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblGame_tblTown_TownId",
                table: "tblGame",
                column: "TownId",
                principalTable: "tblTown",
                principalColumn: "TownId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblGame_tblTown_TownId",
                table: "tblGame");

            migrationBuilder.DropTable(
                name: "tblTown");

            migrationBuilder.DropIndex(
                name: "IX_tblGame_TownId",
                table: "tblGame");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "tblGame");
        }
    }
}
