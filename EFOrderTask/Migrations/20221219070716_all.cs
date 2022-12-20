using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFOrderTask.Migrations
{
    public partial class all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Units_ItemId_Fk",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "Total_Price");

            migrationBuilder.AddColumn<int>(
                name: "Quatity",
                table: "UnitItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CustomerGuidKey",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customer_Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_UnitId_Fk",
                table: "OrderedItems",
                column: "UnitId_Fk");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Units_UnitId_Fk",
                table: "OrderedItems",
                column: "UnitId_Fk",
                principalTable: "Units",
                principalColumn: "Unit_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Units_UnitId_Fk",
                table: "OrderedItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderedItems_UnitId_Fk",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "Quatity",
                table: "UnitItems");

            migrationBuilder.DropColumn(
                name: "CustomerGuidKey",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Customer_Name",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Total_Price",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Units_ItemId_Fk",
                table: "OrderedItems",
                column: "ItemId_Fk",
                principalTable: "Units",
                principalColumn: "Unit_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
