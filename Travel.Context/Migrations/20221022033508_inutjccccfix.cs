using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Context.Migrations
{
    public partial class inutjccccfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostTours_TourDetails_IdCostTour",
                table: "CostTours");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "TourDetails");

            migrationBuilder.DropColumn(
                name: "FinalPriceHoliday",
                table: "TourDetails");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "TourDetails");

            migrationBuilder.DropColumn(
                name: "TotalCostTour",
                table: "TourDetails");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "TourDetails");

            migrationBuilder.RenameColumn(
                name: "PriceHotel",
                table: "CostTours",
                newName: "PriceHotelSR");

            migrationBuilder.RenameColumn(
                name: "IdCostTour",
                table: "CostTours",
                newName: "IdSchedule");

            migrationBuilder.AddColumn<float>(
                name: "AdditionalPrice",
                table: "Tourbookings",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FinalPrice",
                table: "Schedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FinalPriceHoliday",
                table: "Schedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Profit",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalCostTour",
                table: "Schedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Vat",
                table: "Schedules",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PriceHotelDB",
                table: "CostTours",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_CostTours_Schedules_IdSchedule",
                table: "CostTours",
                column: "IdSchedule",
                principalTable: "Schedules",
                principalColumn: "IdSchedule",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostTours_Schedules_IdSchedule",
                table: "CostTours");

            migrationBuilder.DropColumn(
                name: "AdditionalPrice",
                table: "Tourbookings");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "FinalPriceHoliday",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TotalCostTour",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "PriceHotelDB",
                table: "CostTours");

            migrationBuilder.RenameColumn(
                name: "PriceHotelSR",
                table: "CostTours",
                newName: "PriceHotel");

            migrationBuilder.RenameColumn(
                name: "IdSchedule",
                table: "CostTours",
                newName: "IdCostTour");

            migrationBuilder.AddColumn<float>(
                name: "FinalPrice",
                table: "TourDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FinalPriceHoliday",
                table: "TourDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Profit",
                table: "TourDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalCostTour",
                table: "TourDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Vat",
                table: "TourDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_CostTours_TourDetails_IdCostTour",
                table: "CostTours",
                column: "IdCostTour",
                principalTable: "TourDetails",
                principalColumn: "IdTourDetail",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
