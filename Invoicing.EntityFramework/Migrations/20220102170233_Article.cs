using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoicing.EntityFramework.Migrations
{
    public partial class Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UnitPriceIncludingVAT",
                table: "Article",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPriceIncludingVAT",
                table: "Article");
        }
    }
}
