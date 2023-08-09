using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubcontractorsTableCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subcontractors",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<int>(
                name: "SubcontractorId",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Subcontractors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Subcontractors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Subcontractors",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SubcontractorId",
                table: "Tasks",
                column: "SubcontractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Subcontractors_SubcontractorId",
                table: "Tasks",
                column: "SubcontractorId",
                principalTable: "Subcontractors",
                principalColumn: "SubcontractorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Subcontractors_SubcontractorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_SubcontractorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SubcontractorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Subcontractors");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Subcontractors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Subcontractors");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Subcontractors",
                newName: "Name");
        }
    }
}
