using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IssueSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_UserId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_UserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "AssignedId",
                table: "Issues",
                newName: "AssigneeId");

            migrationBuilder.AlterColumn<int>(
                name: "IssueSettingId",
                table: "Issues",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "IssueSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueSettings_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueSettings_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueSettings_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueSettingId",
                table: "Issues",
                column: "IssueSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSettings_ClassId",
                table: "IssueSettings",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSettings_ProjectId",
                table: "IssueSettings",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSettings_SubjectId",
                table: "IssueSettings",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueSettings_IssueSettingId",
                table: "Issues",
                column: "IssueSettingId",
                principalTable: "IssueSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_AssigneeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueSettings_IssueSettingId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "IssueSettings");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_IssueSettingId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Issues",
                newName: "AssignedId");

            migrationBuilder.AlterColumn<int>(
                name: "IssueSettingId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Issues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserId",
                table: "Issues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_UserId",
                table: "Issues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
