using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAndUpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Cart_CartId1",
                table: "ProductCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Product_ProductId1",
                table: "ProductCarts");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_ProductCarts_CartId1",
                table: "ProductCarts");

            migrationBuilder.DropIndex(
                name: "IX_ProductCarts_ProductId1",
                table: "ProductCarts");

            migrationBuilder.DropColumn(
                name: "CartId1",
                table: "ProductCarts");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductCarts");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Order_CartId",
                table: "ProductCarts",
                column: "CartId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCarts_Order_CartId",
                table: "ProductCarts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductCarts");

            migrationBuilder.AddColumn<int>(
                name: "CartId1",
                table: "ProductCarts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarts_CartId1",
                table: "ProductCarts",
                column: "CartId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCarts_ProductId1",
                table: "ProductCarts",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Cart_CartId1",
                table: "ProductCarts",
                column: "CartId1",
                principalTable: "Cart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCarts_Product_ProductId1",
                table: "ProductCarts",
                column: "ProductId1",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
