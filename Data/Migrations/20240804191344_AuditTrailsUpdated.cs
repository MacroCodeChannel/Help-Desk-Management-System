using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuditTrailsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "AuditTrails",
                newName: "PrimaryKey");

            migrationBuilder.AddColumn<string>(
                name: "AffectedColumns",
                table: "AuditTrails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewValues",
                table: "AuditTrails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldValues",
                table: "AuditTrails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffectedColumns",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "NewValues",
                table: "AuditTrails");

            migrationBuilder.DropColumn(
                name: "OldValues",
                table: "AuditTrails");

            migrationBuilder.RenameColumn(
                name: "PrimaryKey",
                table: "AuditTrails",
                newName: "IpAddress");
        }
    }
}
