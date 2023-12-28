using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Migrations
{
    /// <inheritdoc />
    public partial class ProfessorSubjectEnrolledSubjectRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "EnrolledSubjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProfessorSubject",
                columns: table => new
                {
                    ProfessorsId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubject", x => new { x.ProfessorsId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_ProfessorSubject_Professors_ProfessorsId",
                        column: x => x.ProfessorsId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSubject_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledSubjects_ProfessorId",
                table: "EnrolledSubjects",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubject_SubjectsId",
                table: "ProfessorSubject",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrolledSubjects_Professors_ProfessorId",
                table: "EnrolledSubjects",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrolledSubjects_Professors_ProfessorId",
                table: "EnrolledSubjects");

            migrationBuilder.DropTable(
                name: "ProfessorSubject");

            migrationBuilder.DropIndex(
                name: "IX_EnrolledSubjects_ProfessorId",
                table: "EnrolledSubjects");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "EnrolledSubjects");
        }
    }
}
