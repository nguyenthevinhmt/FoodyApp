using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foody.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 23, 20, 14, 830, DateTimeKind.Local).AddTicks(7388));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 23, 20, 14, 830, DateTimeKind.Local).AddTicks(7399));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 23, 20, 14, 830, DateTimeKind.Local).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 23, 20, 14, 830, DateTimeKind.Local).AddTicks(7402));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 24, 23, 20, 14, 830, DateTimeKind.Local).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 24, 23, 20, 14, 820, DateTimeKind.Local).AddTicks(7545), "iW9KTcBCRH4x1K3onkMoFlqg27dOAOCnWR+0UjFswGcWMWp6" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 24, 23, 20, 14, 824, DateTimeKind.Local).AddTicks(3276), "2dlPy+JD4CJMdy9Io/ZZ/idHjcNKLAQICxgoPV9IUvFxNLVF" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 15, 35, 11, 143, DateTimeKind.Local).AddTicks(6414));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 15, 35, 11, 143, DateTimeKind.Local).AddTicks(6418));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 15, 35, 11, 143, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 15, 35, 11, 143, DateTimeKind.Local).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 16, 15, 35, 11, 143, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 16, 15, 35, 11, 135, DateTimeKind.Local).AddTicks(4506), "jMmj/Oc/DyyLCZJH7IcdzWEFSTvJLURSomoolexpAyPnuPyU" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 16, 15, 35, 11, 138, DateTimeKind.Local).AddTicks(7444), "0DoApi8uTIsUWX4rIvTJRaeF22TNCKCprrSWREs1B7f88Dm0" });
        }
    }
}
