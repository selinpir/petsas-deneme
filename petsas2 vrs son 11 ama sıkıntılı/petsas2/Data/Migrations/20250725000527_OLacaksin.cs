using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class OLacaksin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IlId",
                table: "AdresBilgileris",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IlceId",
                table: "AdresBilgileris",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ilces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlId = table.Column<int>(type: "int", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ilces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ilces_ils_IlId",
                        column: x => x.IlId,
                        principalTable: "ils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdresBilgileris_IlceId",
                table: "AdresBilgileris",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdresBilgileris_IlId",
                table: "AdresBilgileris",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_ilces_IlId",
                table: "ilces",
                column: "IlId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdresBilgileris_ilces_IlceId",
                table: "AdresBilgileris",
                column: "IlceId",
                principalTable: "ilces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdresBilgileris_ils_IlId",
                table: "AdresBilgileris",
                column: "IlId",
                principalTable: "ils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdresBilgileris_ilces_IlceId",
                table: "AdresBilgileris");

            migrationBuilder.DropForeignKey(
                name: "FK_AdresBilgileris_ils_IlId",
                table: "AdresBilgileris");

            migrationBuilder.DropTable(
                name: "ilces");

            migrationBuilder.DropTable(
                name: "ils");

            migrationBuilder.DropIndex(
                name: "IX_AdresBilgileris_IlceId",
                table: "AdresBilgileris");

            migrationBuilder.DropIndex(
                name: "IX_AdresBilgileris_IlId",
                table: "AdresBilgileris");

            migrationBuilder.DropColumn(
                name: "IlId",
                table: "AdresBilgileris");

            migrationBuilder.DropColumn(
                name: "IlceId",
                table: "AdresBilgileris");
        }
    }
}
