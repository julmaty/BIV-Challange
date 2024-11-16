using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BIV_Challange.Migrations
{
    /// <inheritdoc />
    public partial class ProductLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableField1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TableField1Cutoffs",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TableField2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TableField2Cutoffs",
                table: "Products");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TableField1",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TableField1Cutoffs",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TableField2",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TableField2Cutoffs",
                table: "Products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
