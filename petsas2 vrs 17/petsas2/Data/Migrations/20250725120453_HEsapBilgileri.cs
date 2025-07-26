using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class HEsapBilgileri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HBilgileri",
                columns: table => new
                {
                    HBId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HesapOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HesapDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinsiyetTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HBilgileri", x => x.HBId);
                    table.ForeignKey(
                        name: "FK_HBilgileri_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HBilgileri_UserId",
                table: "HBilgileri",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HBilgileri");
        }
    }
}
