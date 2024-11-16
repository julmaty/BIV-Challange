using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIV_Challange.Migrations
{
    /// <inheritdoc />
    public partial class CutoffForProductNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CutoffsForProduct_TablesForParams_TableForParamId",
                table: "CutoffsForProduct");

            migrationBuilder.DropIndex(
                name: "IX_CutoffsForProduct_TableForParamId",
                table: "CutoffsForProduct");

            migrationBuilder.DropColumn(
                name: "TableForParamId",
                table: "CutoffsForProduct");

            migrationBuilder.AddColumn<string>(
                name: "CutoffForProductNumbers",
                table: "TablesForParams",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "CutoffsForProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CutoffForProductNumbers",
                table: "TablesForParams");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CutoffsForProduct");

            migrationBuilder.AddColumn<int>(
                name: "TableForParamId",
                table: "CutoffsForProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CutoffsForProduct_TableForParamId",
                table: "CutoffsForProduct",
                column: "TableForParamId");

            migrationBuilder.AddForeignKey(
                name: "FK_CutoffsForProduct_TablesForParams_TableForParamId",
                table: "CutoffsForProduct",
                column: "TableForParamId",
                principalTable: "TablesForParams",
                principalColumn: "Id");
        }
    }
}
