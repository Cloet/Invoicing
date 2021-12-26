using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoicing.EntityFramework.Migrations
{
    public partial class VAT_Description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VAT",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VAT");
        }
    }
}
