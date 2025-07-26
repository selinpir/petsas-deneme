using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class AkifVazgec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AktifMi",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AktifMi",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
