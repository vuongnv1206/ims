using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("cac43a6e-f7bb-4448-baaf-1add431ccbbf"), null, "User role", "User", "USER" },
                    { new Guid("cbc43a8e-f7bb-4445-baaf-1add431ffbbf"), null, "Admin role", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "BirthDay", "ConcurrencyStamp", "CreationTime", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"), 0, null, null, null, "b0d9aeaa-4ece-4672-8b78-f2b00404840d", null, "admin@gmail.com", true, "System", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEA6HWYTA0Wrm8rIaFKXLsgw+Xn7LpAG2hYszDVP4B4Jy7x+rRHtgjQXCpXAaqadBrA==", null, false, "e053f4ff-c705-437a-a41b-e1a1904a52e2", false, "admin@gmail.com" },
                    { new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"), 0, null, null, null, "685bc55e-d355-4b60-a610-0bcea4d59916", null, "user@gmail.com", true, "User", false, null, "USER@GMAIL.COM", "USER@GMAIL.COM", "AQAAAAIAAYagAAAAELy5OhOIcNrBybX6m3P1o28mDx5R1cY+Dt2DctzDPPNCSsTWOmg/Z8v/6wRtKVxwBQ==", null, false, "3f97dc9e-b9d0-4b55-96a8-5bed903c6563", false, "user@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("cbc43a8e-f7bb-4445-baaf-1add431ffbbf"), new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9") },
                    { new Guid("cac43a6e-f7bb-4448-baaf-1add431ccbbf"), new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cbc43a8e-f7bb-4445-baaf-1add431ffbbf"), new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cac43a6e-f7bb-4448-baaf-1add431ccbbf"), new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cac43a6e-f7bb-4448-baaf-1add431ccbbf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbc43a8e-f7bb-4445-baaf-1add431ffbbf"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"));
        }
    }
}
