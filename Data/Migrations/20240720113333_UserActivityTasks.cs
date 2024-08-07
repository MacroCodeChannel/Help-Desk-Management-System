using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserActivityTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SystemTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SystemTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "SystemTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SystemTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemTasks_CreatedById",
                table: "SystemTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTasks_ModifiedById",
                table: "SystemTasks",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemTasks_AspNetUsers_CreatedById",
                table: "SystemTasks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemTasks_AspNetUsers_ModifiedById",
                table: "SystemTasks",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemTasks_AspNetUsers_CreatedById",
                table: "SystemTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemTasks_AspNetUsers_ModifiedById",
                table: "SystemTasks");

            migrationBuilder.DropIndex(
                name: "IX_SystemTasks_CreatedById",
                table: "SystemTasks");

            migrationBuilder.DropIndex(
                name: "IX_SystemTasks_ModifiedById",
                table: "SystemTasks");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SystemTasks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SystemTasks");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "SystemTasks");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SystemTasks");
        }
    }
}
