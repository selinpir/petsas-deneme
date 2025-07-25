using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class AdresBilgileri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AdresBilgileris",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AdresBilgileris");
        }
    }
}
