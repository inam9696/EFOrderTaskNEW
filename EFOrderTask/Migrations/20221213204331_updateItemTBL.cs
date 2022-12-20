using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFOrderTask.Migrations
{
    public partial class updateItemTBL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quatity",
                table: "UnitItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quatity",
                table: "UnitItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
