using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIV_Challange.Migrations
{
    /// <inheritdoc />
    public partial class TableNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OblField3",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OblField4",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "TablesForParams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OblFields",
                table: "Products",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "TablesForParams");

            migrationBuilder.DropColumn(
                name: "OblFields",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "OblField3",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OblField4",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
