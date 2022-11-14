using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Context.Migrations.Travel
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourbookingDetails_Hotels_HotelId",
                table: "tourbookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tourbookingDetails_Places_PlaceId",
                table: "tourbookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tourbookingDetails_Restaurants_RestaurantId",
                table: "tourbookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tourbookingDetails_Tourbookings_IdTourbookingDetails",
                table: "tourbookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Tourbookings_Payment_PaymentId",
                table: "Tourbookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tourbookings_Schedules_ScheduleId",
                table: "Tourbookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tourbookings",
                table: "Tourbookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourbookingDetails",
                table: "tourbookingDetails");

            migrationBuilder.RenameTable(
                name: "Tourbookings",
                newName: "TourBookings");

            migrationBuilder.RenameTable(
                name: "tourbookingDetails",
                newName: "tourBookingDetails");

            migrationBuilder.RenameColumn(
                name: "IdTourbooking",
                table: "TourBookings",
                newName: "IdTourBooking");

            migrationBuilder.RenameIndex(
                name: "IX_Tourbookings_ScheduleId",
                table: "TourBookings",
                newName: "IX_TourBookings_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tourbookings_PaymentId",
                table: "TourBookings",
                newName: "IX_TourBookings_PaymentId");

            migrationBuilder.RenameColumn(
                name: "IdTourbookingDetails",
                table: "tourBookingDetails",
                newName: "IdTourBookingDetails");

            migrationBuilder.RenameIndex(
                name: "IX_tourbookingDetails_RestaurantId",
                table: "tourBookingDetails",
                newName: "IX_tourBookingDetails_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_tourbookingDetails_PlaceId",
                table: "tourBookingDetails",
                newName: "IX_tourBookingDetails_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_tourbookingDetails_HotelId",
                table: "tourBookingDetails",
                newName: "IX_tourBookingDetails_HotelId");

            migrationBuilder.AddColumn<long>(
                name: "ModifyDate",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourBookings",
                table: "TourBookings",
                column: "IdTourBooking");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourBookingDetails",
                table: "tourBookingDetails",
                column: "IdTourBookingDetails");

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", maxLength: 12, nullable: false),
                    IdTour = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tourBookingDetails_Hotels_HotelId",
                table: "tourBookingDetails",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "IdHotel",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourBookingDetails_Places_PlaceId",
                table: "tourBookingDetails",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "IdPlace",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourBookingDetails_Restaurants_RestaurantId",
                table: "tourBookingDetails",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "IdRestaurant",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourBookingDetails_TourBookings_IdTourBookingDetails",
                table: "tourBookingDetails",
                column: "IdTourBookingDetails",
                principalTable: "TourBookings",
                principalColumn: "IdTourBooking",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Payment_PaymentId",
                table: "TourBookings",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "IdPayment",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Schedules_ScheduleId",
                table: "TourBookings",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "IdSchedule",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourBookingDetails_Hotels_HotelId",
                table: "tourBookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tourBookingDetails_Places_PlaceId",
                table: "tourBookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tourBookingDetails_Restaurants_RestaurantId",
                table: "tourBookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_tourBookingDetails_TourBookings_IdTourBookingDetails",
                table: "tourBookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Payment_PaymentId",
                table: "TourBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Schedules_ScheduleId",
                table: "TourBookings");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourBookings",
                table: "TourBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourBookingDetails",
                table: "tourBookingDetails");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "TourBookings",
                newName: "Tourbookings");

            migrationBuilder.RenameTable(
                name: "tourBookingDetails",
                newName: "tourbookingDetails");

            migrationBuilder.RenameColumn(
                name: "IdTourBooking",
                table: "Tourbookings",
                newName: "IdTourbooking");

            migrationBuilder.RenameIndex(
                name: "IX_TourBookings_ScheduleId",
                table: "Tourbookings",
                newName: "IX_Tourbookings_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_TourBookings_PaymentId",
                table: "Tourbookings",
                newName: "IX_Tourbookings_PaymentId");

            migrationBuilder.RenameColumn(
                name: "IdTourBookingDetails",
                table: "tourbookingDetails",
                newName: "IdTourbookingDetails");

            migrationBuilder.RenameIndex(
                name: "IX_tourBookingDetails_RestaurantId",
                table: "tourbookingDetails",
                newName: "IX_tourbookingDetails_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_tourBookingDetails_PlaceId",
                table: "tourbookingDetails",
                newName: "IX_tourbookingDetails_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_tourBookingDetails_HotelId",
                table: "tourbookingDetails",
                newName: "IX_tourbookingDetails_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tourbookings",
                table: "Tourbookings",
                column: "IdTourbooking");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourbookingDetails",
                table: "tourbookingDetails",
                column: "IdTourbookingDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_tourbookingDetails_Hotels_HotelId",
                table: "tourbookingDetails",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "IdHotel",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourbookingDetails_Places_PlaceId",
                table: "tourbookingDetails",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "IdPlace",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourbookingDetails_Restaurants_RestaurantId",
                table: "tourbookingDetails",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "IdRestaurant",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tourbookingDetails_Tourbookings_IdTourbookingDetails",
                table: "tourbookingDetails",
                column: "IdTourbookingDetails",
                principalTable: "Tourbookings",
                principalColumn: "IdTourbooking",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tourbookings_Payment_PaymentId",
                table: "Tourbookings",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "IdPayment",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tourbookings_Schedules_ScheduleId",
                table: "Tourbookings",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "IdSchedule",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
