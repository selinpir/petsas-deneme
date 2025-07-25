using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petsas2.Migrations
{
    /// <inheritdoc />
    public partial class Hasprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Products",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Products");
        }
    }
}
