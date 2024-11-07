using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteManagement.Services.SessionApi.Migrations
{
    /// <inheritdoc />
    public partial class deletednavigationalproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Sessions",
                newName: "Resources");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Sessions",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resources",
                table: "Sessions",
                newName: "Topic");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Sessions",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
