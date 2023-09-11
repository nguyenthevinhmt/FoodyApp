using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFixAndUpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Order_CartId",
                table: "ProductCarts");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarts_OrderId",
                table: "ProductCarts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Order_OrderId",
                table: "ProductCarts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Order_OrderId",
                table: "ProductCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductCarts_OrderId",
                table: "ProductCarts");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Order_CartId",
                table: "ProductCarts",
                column: "CartId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
