using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledSubjects_EnrolledSubjectsEntry_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects");

            migrationBuilder.DropTable(
                name: "EnrolledSubjectsEntry");

            migrationBuilder.RenameColumn(
                name: "EnrolledSubjectsEntryId",
                table: "EnrolledSubjects",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolledSubjects_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects",
                newName: "IX_EnrolledSubjects_EnrollmentId");

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledSubjects_Enrollments_EnrollmentId",
                table: "EnrolledSubjects",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledSubjects_Enrollments_EnrollmentId",
                table: "EnrolledSubjects");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "EnrolledSubjects",
                newName: "EnrolledSubjectsEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrolledSubjects_EnrollmentId",
                table: "EnrolledSubjects",
                newName: "IX_EnrolledSubjects_EnrolledSubjectsEntryId");

            migrationBuilder.CreateTable(
                name: "EnrolledSubjectsEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolledSubjectsEntry", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledSubjects_EnrolledSubjectsEntry_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects",
                column: "EnrolledSubjectsEntryId",
                principalTable: "EnrolledSubjectsEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
