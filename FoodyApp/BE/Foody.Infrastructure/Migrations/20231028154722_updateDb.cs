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
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailTemps_OrderTemps_OrderTempId",
                table: "OrderDetailTemps");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderDetailTemps",
                newName: "referId");

            migrationBuilder.AddColumn<int>(
                name: "CartReferId",
                table: "OrderTemps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderTempId",
                table: "OrderDetailTemps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 47, 22, 231, DateTimeKind.Local).AddTicks(9676));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 47, 22, 231, DateTimeKind.Local).AddTicks(9695));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 47, 22, 231, DateTimeKind.Local).AddTicks(9697));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 47, 22, 231, DateTimeKind.Local).AddTicks(9699));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 47, 22, 231, DateTimeKind.Local).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 28, 22, 47, 22, 169, DateTimeKind.Local).AddTicks(659), "NyxCu3oPnahXCqLsdI6k5XtlaQcmdmY20h3V+6d5zzegM2kI" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 28, 22, 47, 22, 210, DateTimeKind.Local).AddTicks(4704), "Y/ddVEhXVmnr6sq+YmuaLE80SswNkV+a8HgUpA05zgjpowIF" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailTemps_OrderTemps_OrderTempId",
                table: "OrderDetailTemps",
                column: "OrderTempId",
                principalTable: "OrderTemps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailTemps_OrderTemps_OrderTempId",
                table: "OrderDetailTemps");

            migrationBuilder.DropColumn(
                name: "CartReferId",
                table: "OrderTemps");

            migrationBuilder.RenameColumn(
                name: "referId",
                table: "OrderDetailTemps",
                newName: "OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "OrderTempId",
                table: "OrderDetailTemps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 2, 44, 306, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 2, 44, 306, DateTimeKind.Local).AddTicks(1548));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 2, 44, 306, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 2, 44, 306, DateTimeKind.Local).AddTicks(1551));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 28, 22, 2, 44, 306, DateTimeKind.Local).AddTicks(1552));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 28, 22, 2, 44, 293, DateTimeKind.Local).AddTicks(4405), "/dhhOxVbbT9eq6MTVm9UtmzUvNklI4VhhFa5kGZrq7+BnXgr" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2023, 10, 28, 22, 2, 44, 298, DateTimeKind.Local).AddTicks(8317), "jyKMtOb7gJPDGOEhMZimYBlkO3sn/y/YaIDGWBtUH9UKzaBn" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailTemps_OrderTemps_OrderTempId",
                table: "OrderDetailTemps",
                column: "OrderTempId",
                principalTable: "OrderTemps",
                principalColumn: "Id");
        }
    }
}
