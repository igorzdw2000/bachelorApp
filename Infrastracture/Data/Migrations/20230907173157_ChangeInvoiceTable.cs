using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "TotalValue",
                table: "Invoices",
                newName: "TotalToBePaid");

            migrationBuilder.RenameColumn(
                name: "InvoiceDate",
                table: "Invoices",
                newName: "SaleDate");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Invoices",
                newName: "DateOfPayment");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "InvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "InvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "TotalToBePaid",
                table: "Invoices",
                newName: "TotalValue");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Invoices",
                newName: "InvoiceDate");

            migrationBuilder.RenameColumn(
                name: "DateOfPayment",
                table: "Invoices",
                newName: "DueDate");

            migrationBuilder.AddColumn<double>(
                name: "PricePerUnit",
                table: "InvoiceDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
