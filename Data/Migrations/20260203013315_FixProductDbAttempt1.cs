using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEcommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductDbAttempt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceTotal",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceTotal",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
