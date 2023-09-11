using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewFixAndUpdateDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_CategoryId",
                table: "CategoryImages",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryImages_Categories_CategoryId",
                table: "CategoryImages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryImages_Categories_CategoryId",
                table: "CategoryImages");

            migrationBuilder.DropIndex(
                name: "IX_CategoryImages_CategoryId",
                table: "CategoryImages");
        }
    }
}
