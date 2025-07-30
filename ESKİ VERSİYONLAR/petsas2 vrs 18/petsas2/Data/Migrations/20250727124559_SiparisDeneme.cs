using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class SiparisDeneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiparisDb",
                columns: table => new
                {
                    SiparisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    S_ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SiparisDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KartIsim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OdemeYontemi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HesapBilgileriId = table.Column<int>(type: "int", nullable: false),
                    AdresId = table.Column<int>(type: "int", nullable: false),
                    ABId = table.Column<int>(type: "int", nullable: false),
                    HBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisDb", x => x.SiparisID);
                    table.ForeignKey(
                        name: "FK_SiparisDb_ABilgileri_AdresId",
                        column: x => x.AdresId,
                        principalTable: "ABilgileri",
                        principalColumn: "ABId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiparisDb_HBilgileri_HesapBilgileriId",
                        column: x => x.HesapBilgileriId,
                        principalTable: "HBilgileri",
                        principalColumn: "HBId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaturaDb",
                columns: table => new
                {
                    FaturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaturaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MusteriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    F_ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FaturaNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SiparisNo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OdemeYontemi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaticiUnvan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaticiAdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaticiAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaticiVergiNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiparisID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturaDb", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_FaturaDb_SiparisDb_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SiparisDb",
                        principalColumn: "SiparisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OdemeDb",
                columns: table => new
                {
                    OdemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdemeTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    O_ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    KartIsim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OdemeYontemi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiparisID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdemeDb", x => x.OdemeId);
                    table.ForeignKey(
                        name: "FK_OdemeDb_SiparisDb_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SiparisDb",
                        principalColumn: "SiparisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiparisDetayDb",
                columns: table => new
                {
                    SiparisDetayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisID = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    UrunMiktar = table.Column<int>(type: "int", nullable: false),
                    BirimFiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FaturaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiparisDetayDb", x => x.SiparisDetayId);
                    table.ForeignKey(
                        name: "FK_SiparisDetayDb_FaturaDb_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "FaturaDb",
                        principalColumn: "FaturaId");
                    table.ForeignKey(
                        name: "FK_SiparisDetayDb_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SiparisDetayDb_SiparisDb_SiparisID",
                        column: x => x.SiparisID,
                        principalTable: "SiparisDb",
                        principalColumn: "SiparisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDb_SiparisID",
                table: "FaturaDb",
                column: "SiparisID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OdemeDb_SiparisID",
                table: "OdemeDb",
                column: "SiparisID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDb_AdresId",
                table: "SiparisDb",
                column: "AdresId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDb_HesapBilgileriId",
                table: "SiparisDb",
                column: "HesapBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetayDb_FaturaId",
                table: "SiparisDetayDb",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetayDb_ProductId",
                table: "SiparisDetayDb",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetayDb_SiparisID",
                table: "SiparisDetayDb",
                column: "SiparisID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdemeDb");

            migrationBuilder.DropTable(
                name: "SiparisDetayDb");

            migrationBuilder.DropTable(
                name: "FaturaDb");

            migrationBuilder.DropTable(
                name: "SiparisDb");
        }
    }
}
