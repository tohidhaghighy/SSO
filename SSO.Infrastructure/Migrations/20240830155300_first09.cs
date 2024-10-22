using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8769), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8757) });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8771), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8771) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8846), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8847) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8848), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8848) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8849), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8849) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8850), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8851) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8853), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8853) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8854), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8854) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8855), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8855) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApplicationId", "InsertDate", "RefreshTokenExpiryTime", "UpdateDate" },
                values: new object[] { 1, new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8868), new DateTime(2027, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8871), new DateTime(2024, 8, 30, 19, 23, 0, 627, DateTimeKind.Local).AddTicks(8867) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8962), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8964), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8963) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9037), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9038) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9039), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9039) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9040), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9040) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9041), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9041) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9042), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9042) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9043), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9043) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "InsertDate", "UpdateDate" },
                values: new object[] { new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9044), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9044) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ApplicationId", "InsertDate", "RefreshTokenExpiryTime", "UpdateDate" },
                values: new object[] { 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9056), new DateTime(2027, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9060), new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9056) });
        }
    }
}
