using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreationTime_AppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash" },
                values: new object[] { "eadf5a72-f169-47cd-b441-7fe648bc0e19", null, "AQAAAAIAAYagAAAAEE/9lFT9fE98HQZEzff25ynzoE1oVoR9RFzA/bXrWi/rLz6L/HcpWQubJxjD+13vTg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash" },
                values: new object[] { "177ac888-1802-470c-a2aa-7ecf2fd09079", null, "AQAAAAIAAYagAAAAEMRZF5BkRe707W3smWNagRaZ4vLJcV6jy+cDBaLtMik9LyXOmz+PImDVnm+iuLgTFw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d391699-09b8-4ca4-80e3-c02d0ffa41cf", "AQAAAAIAAYagAAAAENKOFux+/duq/F7j0fnga1eFDqT5UWb0XRpY4xbMPm2uJk/UTMpIpSx/hNZ4klDACQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e6c3d397-f209-414c-a64b-b64dc05a55c7", "AQAAAAIAAYagAAAAEILViuhmkJoj//31ixllFQnJE40Ute7fPsGodq+WU6ep9vegxZtCkmc/bJ7ff5madg==" });
        }
    }
}
