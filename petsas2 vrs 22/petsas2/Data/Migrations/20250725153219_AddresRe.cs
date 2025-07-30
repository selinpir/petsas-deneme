using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class AddresRe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ABilgileri",
                columns: table => new
                {
                    ABId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HesapBilgileriId = table.Column<int>(type: "int", nullable: false),
                    IlAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdresAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcikAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABilgileri", x => x.ABId);
                    table.ForeignKey(
                        name: "FK_ABilgileri_HBilgileri_HesapBilgileriId",
                        column: x => x.HesapBilgileriId,
                        principalTable: "HBilgileri",
                        principalColumn: "HBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABilgileri_HesapBilgileriId",
                table: "ABilgileri",
                column: "HesapBilgileriId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ABilgileri");
        }
    }
}
