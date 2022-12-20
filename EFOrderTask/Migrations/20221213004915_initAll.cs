using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFOrderTask.Migrations
{
    public partial class initAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Item_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Item_Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Unit_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Unit_Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderedItems",
                columns: table => new
                {
                    ItemId_Fk = table.Column<int>(type: "int", nullable: false),
                    OrderId_FK = table.Column<int>(type: "int", nullable: false),
                    UnitId_Fk = table.Column<int>(type: "int", nullable: false),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Sub_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedItems", x => new { x.OrderId_FK, x.ItemId_Fk, x.UnitId_Fk });
                    table.ForeignKey(
                        name: "FK_OrderedItems_Items_ItemId_Fk",
                        column: x => x.ItemId_Fk,
                        principalTable: "Items",
                        principalColumn: "Item_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedItems_Orders_OrderId_FK",
                        column: x => x.OrderId_FK,
                        principalTable: "Orders",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedItems_Units_ItemId_Fk",
                        column: x => x.ItemId_Fk,
                        principalTable: "Units",
                        principalColumn: "Unit_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitItems",
                columns: table => new
                {
                    ItemId_FK = table.Column<int>(type: "int", nullable: false),
                    UnitId_FK = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quatity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitItems", x => new { x.UnitId_FK, x.ItemId_FK });
                    table.ForeignKey(
                        name: "FK_UnitItems_Items_ItemId_FK",
                        column: x => x.ItemId_FK,
                        principalTable: "Items",
                        principalColumn: "Item_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitItems_Units_UnitId_FK",
                        column: x => x.UnitId_FK,
                        principalTable: "Units",
                        principalColumn: "Unit_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_ItemId_Fk",
                table: "OrderedItems",
                column: "ItemId_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_UnitItems_ItemId_FK",
                table: "UnitItems",
                column: "ItemId_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedItems");

            migrationBuilder.DropTable(
                name: "UnitItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
