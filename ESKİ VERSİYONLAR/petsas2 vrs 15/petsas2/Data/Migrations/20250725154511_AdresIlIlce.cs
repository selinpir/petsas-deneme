using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class AdresIlIlce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IlAdi",
                table: "ABilgileri");

            migrationBuilder.DropColumn(
                name: "IlceAdi",
                table: "ABilgileri");

            migrationBuilder.AddColumn<int>(
                name: "IlId",
                table: "ABilgileri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IlceId",
                table: "ABilgileri",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IlDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IlceDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlceDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlceDb_IlDb_IlId",
                        column: x => x.IlId,
                        principalTable: "IlDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABilgileri_IlceId",
                table: "ABilgileri",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_ABilgileri_IlId",
                table: "ABilgileri",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_IlceDb_IlId",
                table: "IlceDb",
                column: "IlId");

            migrationBuilder.AddForeignKey(
                name: "FK_ABilgileri_IlDb_IlId",
                table: "ABilgileri",
                column: "IlId",
                principalTable: "IlDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ABilgileri_IlceDb_IlceId",
                table: "ABilgileri",
                column: "IlceId",
                principalTable: "IlceDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ABilgileri_IlDb_IlId",
                table: "ABilgileri");

            migrationBuilder.DropForeignKey(
                name: "FK_ABilgileri_IlceDb_IlceId",
                table: "ABilgileri");

            migrationBuilder.DropTable(
                name: "IlceDb");

            migrationBuilder.DropTable(
                name: "IlDb");

            migrationBuilder.DropIndex(
                name: "IX_ABilgileri_IlceId",
                table: "ABilgileri");

            migrationBuilder.DropIndex(
                name: "IX_ABilgileri_IlId",
                table: "ABilgileri");

            migrationBuilder.DropColumn(
                name: "IlId",
                table: "ABilgileri");

            migrationBuilder.DropColumn(
                name: "IlceId",
                table: "ABilgileri");

            migrationBuilder.AddColumn<string>(
                name: "IlAdi",
                table: "ABilgileri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IlceAdi",
                table: "ABilgileri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
