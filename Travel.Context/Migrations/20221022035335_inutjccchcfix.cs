using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Context.Migrations
{
    public partial class inutjccchcfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AdditionalPrice",
                table: "Schedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "AdditionalPriceHoliday",
                table: "Schedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalPrice",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "AdditionalPriceHoliday",
                table: "Schedules");
        }
    }
}
