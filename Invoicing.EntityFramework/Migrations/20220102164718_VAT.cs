using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoicing.EntityFramework.Migrations
{
    public partial class VAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_VAT_VATId",
                table: "Article");

            migrationBuilder.AlterColumn<int>(
                name: "VATId",
                table: "Article",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_VAT_VATId",
                table: "Article",
                column: "VATId",
                principalTable: "VAT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_VAT_VATId",
                table: "Article");

            migrationBuilder.AlterColumn<int>(
                name: "VATId",
                table: "Article",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_VAT_VATId",
                table: "Article",
                column: "VATId",
                principalTable: "VAT",
                principalColumn: "Id");
        }
    }
}
