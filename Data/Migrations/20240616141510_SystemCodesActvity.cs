using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SystemCodesActvity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodeDetails_SystemCodes_SystemCodeId",
                table: "SystemCodeDetails");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SystemCodes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SystemCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "SystemCodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SystemCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SystemCodeDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SystemCodeDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "SystemCodeDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SystemCodeDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodes_CreatedById",
                table: "SystemCodes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodes_ModifiedById",
                table: "SystemCodes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodeDetails_CreatedById",
                table: "SystemCodeDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodeDetails_ModifiedById",
                table: "SystemCodeDetails",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodeDetails_AspNetUsers_CreatedById",
                table: "SystemCodeDetails",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodeDetails_AspNetUsers_ModifiedById",
                table: "SystemCodeDetails",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodeDetails_SystemCodes_SystemCodeId",
                table: "SystemCodeDetails",
                column: "SystemCodeId",
                principalTable: "SystemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodes_AspNetUsers_CreatedById",
                table: "SystemCodes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodes_AspNetUsers_ModifiedById",
                table: "SystemCodes",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodeDetails_AspNetUsers_CreatedById",
                table: "SystemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodeDetails_AspNetUsers_ModifiedById",
                table: "SystemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodeDetails_SystemCodes_SystemCodeId",
                table: "SystemCodeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodes_AspNetUsers_CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodes_AspNetUsers_ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodes_CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodes_ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodeDetails_CreatedById",
                table: "SystemCodeDetails");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodeDetails_ModifiedById",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "SystemCodeDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SystemCodeDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodeDetails_SystemCodes_SystemCodeId",
                table: "SystemCodeDetails",
                column: "SystemCodeId",
                principalTable: "SystemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
