using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Migrations
{
    /// <inheritdoc />
    public partial class EnrolledSubjectsEntryCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrolledSubjectsEntryId",
                table: "EnrolledSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledSubjects_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects",
                column: "EnrolledSubjectsEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledSubjects_EnrolledSubjectsEntry_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects",
                column: "EnrolledSubjectsEntryId",
                principalTable: "EnrolledSubjectsEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledSubjects_EnrolledSubjectsEntry_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects");

            migrationBuilder.DropTable(
                name: "EnrolledSubjectsEntry");

            migrationBuilder.DropIndex(
                name: "IX_EnrolledSubjects_EnrolledSubjectsEntryId",
                table: "EnrolledSubjects");

            migrationBuilder.DropColumn(
                name: "EnrolledSubjectsEntryId",
                table: "EnrolledSubjects");
        }
    }
}
