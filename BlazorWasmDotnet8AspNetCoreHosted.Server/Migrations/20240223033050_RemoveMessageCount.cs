﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorWasmDotnet8AspNetCoreHosted.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMessageCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageCount",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageCount",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
