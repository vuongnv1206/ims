using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class Update_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SubjectUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Subjects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Subjects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Settings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Projects",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProjectMembers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Milestones",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IssueSettings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Issues",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ClassStudents",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Classes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Assignments",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "407d2e60-410c-438e-b3a1-ff14ef2dec90", "AQAAAAIAAYagAAAAEM6WyTmmmhV9SWZnCPqtCgLqDMf7ST+tRVIknmNI7SmD4k+LZdu19wZP4OGYt49Nkw==", "bba12435-928f-4926-90d0-0ff3009a5149" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8534f8fc-eb77-4667-812c-f7a711718c92", "AQAAAAIAAYagAAAAEIQt1Gp/F+pLbHho9f/kTKLf6U/PuvZ1NX/8QdRzklqJ2bo/ZsC4cG2phI3KTTi7Cg==", "2e257193-654d-46b6-b10c-2986c18797f2" });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ManagerId",
                table: "Subjects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AssigneeId",
                table: "Classes",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_AspNetUsers_AssigneeId",
                table: "Classes",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_ManagerId",
                table: "Subjects",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_AspNetUsers_AssigneeId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_ManagerId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ManagerId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Classes_AssigneeId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SubjectUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IssueSettings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ClassStudents");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Assignments");


            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0d9aeaa-4ece-4672-8b78-f2b00404840d", "AQAAAAIAAYagAAAAEA6HWYTA0Wrm8rIaFKXLsgw+Xn7LpAG2hYszDVP4B4Jy7x+rRHtgjQXCpXAaqadBrA==", "e053f4ff-c705-437a-a41b-e1a1904a52e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e224968-33e4-4652-b7b7-8574d048cdb9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "685bc55e-d355-4b60-a610-0bcea4d59916", "AQAAAAIAAYagAAAAELy5OhOIcNrBybX6m3P1o28mDx5R1cY+Dt2DctzDPPNCSsTWOmg/Z8v/6wRtKVxwBQ==", "3f97dc9e-b9d0-4b55-96a8-5bed903c6563" });

        }
    }
}
