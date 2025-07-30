using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class Sepet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KullaniciSepets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    SepetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciSepets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sepets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    UrunAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gorsel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrutFiyat = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false),
                    KullaniciSepetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sepets_KullaniciSepets_KullaniciSepetId",
                        column: x => x.KullaniciSepetId,
                        principalTable: "KullaniciSepets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sepets_KullaniciSepetId",
                table: "Sepets",
                column: "KullaniciSepetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sepets");

            migrationBuilder.DropTable(
                name: "KullaniciSepets");
        }
    }
}
