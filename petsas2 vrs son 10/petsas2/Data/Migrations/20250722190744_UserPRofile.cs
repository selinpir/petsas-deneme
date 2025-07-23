using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class UserPRofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hesapBilgileris",
                columns: table => new
                {
                    HBId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DogumTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HesapOlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HesapDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinsiyetTipi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hesapBilgileris", x => x.HBId);
                    table.ForeignKey(
                        name: "FK_hesapBilgileris_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdresBilgileris",
                columns: table => new
                {
                    ABId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HesapBilgileriId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    il_id = table.Column<int>(type: "int", nullable: false),
                    AcikAdres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdresBilgileris", x => x.ABId);
                    table.ForeignKey(
                        name: "FK_AdresBilgileris_hesapBilgileris_HesapBilgileriId",
                        column: x => x.HesapBilgileriId,
                        principalTable: "hesapBilgileris",
                        principalColumn: "HBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetBilgileri",
                columns: table => new
                {
                    PBId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HesapBilgileriId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GorselUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetDogum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EkMetin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetBilgileri", x => x.PBId);
                    table.ForeignKey(
                        name: "FK_PetBilgileri_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetBilgileri_hesapBilgileris_HesapBilgileriId",
                        column: x => x.HesapBilgileriId,
                        principalTable: "hesapBilgileris",
                        principalColumn: "HBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdresBilgileris_HesapBilgileriId",
                table: "AdresBilgileris",
                column: "HesapBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_hesapBilgileris_UserId",
                table: "hesapBilgileris",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PetBilgileri_CategoryId",
                table: "PetBilgileri",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PetBilgileri_HesapBilgileriId",
                table: "PetBilgileri",
                column: "HesapBilgileriId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdresBilgileris");

            migrationBuilder.DropTable(
                name: "PetBilgileri");

            migrationBuilder.DropTable(
                name: "hesapBilgileris");
        }
    }
}
