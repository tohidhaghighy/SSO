using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authentication_Server.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "GeneratedKey", "Icon", "InnerLink", "InsertDate", "InsertUserName", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "123456789", "", "", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8962), "admin", "سامانه SSO", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8950) },
                    { 2, "1234", "", "", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8964), "admin", "سامانه تیکتینگ ویرا", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(8963) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ApplicationId", "InsertDate", "InsertUserName", "Name", "UpdateDate", "UrlPanel" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9037), "admin", "ادمین سامانه SSO", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9038), "https://localhost:7062/Home/Index" },
                    { 2, 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9039), "admin", "ادمین سامانه مدیریت پرونده ها", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9039), "http://localhost:3000/api" },
                    { 3, 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9040), "admin", "ادمین سامانه میز خدمت", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9040), "http://localhost:3000/api" },
                    { 4, 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9041), "admin", "ادمین تعزیرات", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9041), "http://localhost:3000/api" },
                    { 5, 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9042), "admin", "ادمین ویرا", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9042), "http://localhost:3000/api" },
                    { 6, 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9043), "admin", "ادمین سامانه امحا", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9043), "http://localhost:3000/api" },
                    { 7, 2, new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9044), "admin", "ادمین سامانه تبادل اطلاعات", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9044), "http://localhost:3000/api" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ApplicationId", "Email", "Family", "Image", "InsertDate", "InsertUserName", "Mobile", "Name", "Password", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Token", "UpdateDate", "UserName" },
                values: new object[] { 1, true, 2, "", "superuser", "", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9056), "admin", "", "superuser", "aosxGe1VJ8FIz3wnfJTdYnV1sl/swHqF7NbWdmdgkIc=", "t6UHmvVLRkg2d0OsvTR0gdkAY77slk59Ryww0SvsBTc=", new DateTime(2027, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9060), 1, "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxNCIsIlVzZXJuYW1lIjoic3VwZXJ1c2VyIiwiVXNlclJvbGUiOiIxMCIsImV4cCI6MTcyNTIwMzQyNiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDoyODc0Ny8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjI4NzQ3LyJ9.RYvoJhqSnZHcLaoKKXotdms4dJBNGRazEaqoYONdMAQ", new DateTime(2024, 8, 30, 19, 16, 50, 203, DateTimeKind.Local).AddTicks(9056), "superUser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
