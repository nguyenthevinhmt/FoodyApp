using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFixAndUpdateDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Order_OrderId",
                table: "ProductCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductCarts_OrderId",
                table: "ProductCarts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductCarts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarts_OrderId",
                table: "ProductCarts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Order_OrderId",
                table: "ProductCarts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
