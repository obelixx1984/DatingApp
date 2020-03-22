using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class AddedLubieEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lajki",
                columns: table => new
                {
                    LubiId = table.Column<int>(nullable: false),
                    LubiiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lajki", x => new { x.LubiId, x.LubiiId });
                    table.ForeignKey(
                        name: "FK_Lajki_Users_LubiId",
                        column: x => x.LubiId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lajki_Users_LubiiId",
                        column: x => x.LubiiId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lajki_LubiiId",
                table: "Lajki",
                column: "LubiiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lajki");
        }
    }
}
