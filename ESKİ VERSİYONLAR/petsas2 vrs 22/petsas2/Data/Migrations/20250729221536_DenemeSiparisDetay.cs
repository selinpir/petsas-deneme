using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class DenemeSiparisDetay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetayDb_Products_ProductId",
                table: "SiparisDetayDb");

            migrationBuilder.DropIndex(
                name: "IX_SiparisDetayDb_ProductId",
                table: "SiparisDetayDb");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SiparisDetayDb");

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetayDb_UrunId",
                table: "SiparisDetayDb",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetayDb_Products_UrunId",
                table: "SiparisDetayDb",
                column: "UrunId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiparisDetayDb_Products_UrunId",
                table: "SiparisDetayDb");

            migrationBuilder.DropIndex(
                name: "IX_SiparisDetayDb_UrunId",
                table: "SiparisDetayDb");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SiparisDetayDb",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiparisDetayDb_ProductId",
                table: "SiparisDetayDb",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiparisDetayDb_Products_ProductId",
                table: "SiparisDetayDb",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
