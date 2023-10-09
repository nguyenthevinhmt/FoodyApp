using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class suadb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Carts");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 58, 59, 449, DateTimeKind.Local).AddTicks(6114));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 58, 59, 449, DateTimeKind.Local).AddTicks(6123));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 58, 59, 449, DateTimeKind.Local).AddTicks(6124));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 58, 59, 449, DateTimeKind.Local).AddTicks(6125));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 58, 59, 449, DateTimeKind.Local).AddTicks(6127));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 9, 17, 58, 59, 440, DateTimeKind.Local).AddTicks(5899), "aTX8Of8eInsgnFTlNEhF8i+8CwMgqxWDS1BgNPpFcoZneFsm" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 9, 17, 58, 59, 444, DateTimeKind.Local).AddTicks(1270), "PmjHdm/E7zL5XI4XXCSNEgBXyW//txtPrY382n7eDOXqPK08" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 12, 59, 941, DateTimeKind.Local).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 12, 59, 941, DateTimeKind.Local).AddTicks(7658));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 12, 59, 941, DateTimeKind.Local).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 12, 59, 941, DateTimeKind.Local).AddTicks(7661));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 9, 17, 12, 59, 941, DateTimeKind.Local).AddTicks(7662));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 9, 17, 12, 59, 931, DateTimeKind.Local).AddTicks(9454), "a+SHSTtJgwCPKy93ZTAzpir5xkHI+HQq6v51SEiRCAW1uCoD" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 9, 17, 12, 59, 935, DateTimeKind.Local).AddTicks(9891), "5IAmya3fSynuomKvjaWPvnFUroI5MtnqmQujk2LqsdVy8RyX" });
        }
    }
}
