using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class WiadomosciEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wiadomosc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WyslalId = table.Column<int>(nullable: false),
                    OdbiorcaId = table.Column<int>(nullable: false),
                    Tresc = table.Column<string>(nullable: true),
                    JestCzytana = table.Column<bool>(nullable: false),
                    DataCzytania = table.Column<DateTime>(nullable: true),
                    DataWyslania = table.Column<DateTime>(nullable: false),
                    WysylajacyUsunal = table.Column<bool>(nullable: false),
                    OdbiorcaUsunal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wiadomosc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wiadomosc_Users_OdbiorcaId",
                        column: x => x.OdbiorcaId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wiadomosc_Users_WyslalId",
                        column: x => x.WyslalId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_OdbiorcaId",
                table: "Wiadomosc",
                column: "OdbiorcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wiadomosc_WyslalId",
                table: "Wiadomosc",
                column: "WyslalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wiadomosc");
        }
    }
}
