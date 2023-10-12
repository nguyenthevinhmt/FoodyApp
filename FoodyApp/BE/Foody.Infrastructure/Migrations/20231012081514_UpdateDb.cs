using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImageUrl",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "AddressType",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DetailAddress",
                table: "Order",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Order",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 12, 15, 15, 13, 976, DateTimeKind.Local).AddTicks(3231));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 12, 15, 15, 13, 976, DateTimeKind.Local).AddTicks(3242));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 12, 15, 15, 13, 976, DateTimeKind.Local).AddTicks(3243));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 12, 15, 15, 13, 976, DateTimeKind.Local).AddTicks(3244));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 12, 15, 15, 13, 976, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 12, 15, 15, 13, 967, DateTimeKind.Local).AddTicks(8195), "Du+2n4ogXkSIVFc6pICvFfb8p5aURj7WddOyubphntexChUA" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 12, 15, 15, 13, 970, DateTimeKind.Local).AddTicks(9496), "9BZbMJMmR1OShgxTJdxAIa4ffiBq0eUV/tEWmr5HC/4LymXH" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DetailAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "CategoryImageUrl",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryImageUrl", "CreatedAt" },
                values: new object[] { "no-image.png", new DateTime(2023, 10, 12, 13, 41, 9, 715, DateTimeKind.Local).AddTicks(9924) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryImageUrl", "CreatedAt" },
                values: new object[] { "no-image.png", new DateTime(2023, 10, 12, 13, 41, 9, 715, DateTimeKind.Local).AddTicks(9930) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryImageUrl", "CreatedAt" },
                values: new object[] { "no-image.png", new DateTime(2023, 10, 12, 13, 41, 9, 715, DateTimeKind.Local).AddTicks(9931) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryImageUrl", "CreatedAt" },
                values: new object[] { "no-image.png", new DateTime(2023, 10, 12, 13, 41, 9, 715, DateTimeKind.Local).AddTicks(9932) });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryImageUrl", "CreatedAt" },
                values: new object[] { "no-image.png", new DateTime(2023, 10, 12, 13, 41, 9, 715, DateTimeKind.Local).AddTicks(9934) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 12, 13, 41, 9, 706, DateTimeKind.Local).AddTicks(5593), "X+U+REnr0w2PW6Zf9km8cAVzhW/j5Y9rEKoltlBwyD9UKTao" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 12, 13, 41, 9, 710, DateTimeKind.Local).AddTicks(3956), "V5H94b85MbaYO+1oRNoIBBVvhRcUlnROeGvr1HFZYFOfBQwv" });
        }
    }
}
