using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Migrations
{
    /// <inheritdoc />
    public partial class OwnerIdToGroupChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "GroupChats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "GroupChats");
        }
    }
}
