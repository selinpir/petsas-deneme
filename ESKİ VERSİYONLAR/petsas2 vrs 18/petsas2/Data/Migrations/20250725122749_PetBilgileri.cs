using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class PetBilgileri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PBilgileri",
                columns: table => new
                {
                    PBId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GorselUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetDogum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EkMetin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HesapBilgileriId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PBilgileri", x => x.PBId);
                    table.ForeignKey(
                        name: "FK_PBilgileri_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PBilgileri_HBilgileri_HesapBilgileriId",
                        column: x => x.HesapBilgileriId,
                        principalTable: "HBilgileri",
                        principalColumn: "HBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PBilgileri_CategoryId",
                table: "PBilgileri",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PBilgileri_HesapBilgileriId",
                table: "PBilgileri",
                column: "HesapBilgileriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PBilgileri");
        }
    }
}
