using Microsoft.EntityFrameworkCore.Migrations;

namespace Ticket.Infrastructure.Persistence.Migrations
{
    public partial class EquipmentIdMissing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                schema: "Ticket",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EquipmentId",
                schema: "Ticket",
                table: "Equipments");
        }
    }
}
