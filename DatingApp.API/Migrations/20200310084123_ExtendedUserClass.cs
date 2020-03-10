using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingApp.API.Migrations
{
    public partial class ExtendedUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KogoSzukasz",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kraj",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Miasto",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OstatnioAktywny",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Plec",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tytul",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Urodziny",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Utworzony",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Wprowadzony",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zainteresowania",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Zdjecia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    DataDodania = table.Column<DateTime>(nullable: false),
                    ToMenu = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zdjecia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zdjecia_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zdjecia_UserId",
                table: "Zdjecia",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zdjecia");

            migrationBuilder.DropColumn(
                name: "KogoSzukasz",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Kraj",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Miasto",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OstatnioAktywny",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Plec",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Tytul",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Urodziny",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Utworzony",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Wprowadzony",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Zainteresowania",
                table: "Users");
        }
    }
}
