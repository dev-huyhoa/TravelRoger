﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Context.Migrations
{
    public partial class onnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    IdBanner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBanner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.IdBanner);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    IdCar = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LiscensePlate = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AmountSeat = table.Column<int>(type: "int", nullable: false),
                    NameDriver = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.IdCar);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    IdContract = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameContract = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TypeService = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SignDate = table.Column<long>(type: "bigint", nullable: false),
                    ExpDate = table.Column<long>(type: "bigint", nullable: false),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.IdContract);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdCustomer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameCustomer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Birthday = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    Point = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    FbToken = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    GoogleToken = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdCustomer);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    IdFile = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.IdFile);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    IdHotel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameHotel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Star = table.Column<int>(type: "int", nullable: false),
                    Approve = table.Column<int>(type: "int", nullable: false),
                    SingleRoomPrice = table.Column<float>(type: "real", nullable: false),
                    DoubleRoomPrice = table.Column<float>(type: "real", nullable: false),
                    QuantityDBR = table.Column<int>(type: "int", nullable: false),
                    QuantitySR = table.Column<int>(type: "int", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.IdHotel);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    IdImage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IdService = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.IdImage);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePayment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.IdPayment);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    IdPlace = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamePlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Approve = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PriceTicket = table.Column<float>(type: "real", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.IdPlace);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    IdPromotion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ToDate = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<long>(type: "bigint", nullable: false),
                    Approve = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.IdPromotion);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    IdProvince = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameProvince = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.IdProvince);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    IdRestaurant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameRestaurant = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ComboPrice = table.Column<float>(type: "real", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Approve = table.Column<int>(type: "int", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.IdRestaurant);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    IdTour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameTour = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    FromPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ToPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ApproveStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Thumbsnail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.IdTour);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Point = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.IdVoucher);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    IdDistrict = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameDistrict = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.IdDistrict);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "IdProvince",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmployee = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameEmployee = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Birthday = table.Column<long>(type: "bigint", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourDetails",
                columns: table => new
                {
                    IdTourDetail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TourId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    QuantityBooked = table.Column<int>(type: "int", nullable: false),
                    TotalCostTour = table.Column<float>(type: "real", nullable: false),
                    Profit = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<float>(type: "real", nullable: false),
                    FinalPrice = table.Column<float>(type: "real", nullable: false),
                    FinalPriceHoliday = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDetails", x => x.IdTourDetail);
                    table.ForeignKey(
                        name: "FK_TourDetails_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "IdTour",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    IdWard = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameWard = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.IdWard);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "IdDistrict",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    IdSchedule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DepartureDate = table.Column<long>(type: "bigint", nullable: false),
                    ReturnDate = table.Column<long>(type: "bigint", nullable: false),
                    BeginDate = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<long>(type: "bigint", nullable: false),
                    TimePromotion = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Approve = table.Column<int>(type: "int", nullable: false),
                    QuantityAdult = table.Column<float>(type: "real", nullable: false),
                    QuantityBaby = table.Column<float>(type: "real", nullable: false),
                    MinCapacity = table.Column<int>(type: "int", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    QuantityChild = table.Column<float>(type: "real", nullable: false),
                    Isdelete = table.Column<bool>(type: "bit", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false),
                    TourId = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.IdSchedule);
                    table.ForeignKey(
                        name: "FK_Schedules_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "IdCar",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "IdPromotion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "IdTour",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostTours",
                columns: table => new
                {
                    IdCostTour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Breakfast = table.Column<float>(type: "real", nullable: false),
                    Water = table.Column<float>(type: "real", nullable: false),
                    FeeGas = table.Column<float>(type: "real", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    SellCost = table.Column<float>(type: "real", nullable: false),
                    Depreciation = table.Column<float>(type: "real", nullable: false),
                    OtherPrice = table.Column<float>(type: "real", nullable: false),
                    Tolls = table.Column<float>(type: "real", nullable: false),
                    CusExpected = table.Column<int>(type: "int", nullable: false),
                    InsuranceFee = table.Column<float>(type: "real", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false),
                    TotalCostTour = table.Column<float>(type: "real", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceHotel = table.Column<float>(type: "real", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceRestaurant = table.Column<float>(type: "real", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceTicketPlace = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostTours", x => x.IdCostTour);
                    table.ForeignKey(
                        name: "FK_CostTours_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "IdHotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostTours_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "IdPlace",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostTours_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "IdRestaurant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostTours_TourDetails_IdCostTour",
                        column: x => x.IdCostTour,
                        principalTable: "TourDetails",
                        principalColumn: "IdTourDetail",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timelines",
                columns: table => new
                {
                    IdTimeline = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FromTime = table.Column<long>(type: "bigint", nullable: false),
                    ToTime = table.Column<long>(type: "bigint", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IdSchedule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timelines", x => x.IdTimeline);
                    table.ForeignKey(
                        name: "FK_Timelines_Schedules_IdSchedule",
                        column: x => x.IdSchedule,
                        principalTable: "Schedules",
                        principalColumn: "IdSchedule",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tourbookings",
                columns: table => new
                {
                    IdTourbooking = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameCustomer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameContact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    BookingNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateBooking = table.Column<long>(type: "bigint", nullable: false),
                    LastDate = table.Column<long>(type: "bigint", nullable: false),
                    Vat = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VoucherCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ValuePromotion = table.Column<int>(type: "int", nullable: false),
                    IsCalled = table.Column<bool>(type: "bit", nullable: false),
                    Deposit = table.Column<float>(type: "real", nullable: false),
                    RemainPrice = table.Column<float>(type: "real", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    TotalPricePromotion = table.Column<float>(type: "real", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifyDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourbookings", x => x.IdTourbooking);
                    table.ForeignKey(
                        name: "FK_Tourbookings_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "IdPayment",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tourbookings_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "IdSchedule",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tourbookingDetails",
                columns: table => new
                {
                    IdTourbookingDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Baby = table.Column<int>(type: "int", nullable: false),
                    Child = table.Column<int>(type: "int", nullable: false),
                    Adult = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCalled = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourbookingDetails", x => x.IdTourbookingDetails);
                    table.ForeignKey(
                        name: "FK_tourbookingDetails_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "IdHotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tourbookingDetails_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "IdPlace",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tourbookingDetails_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "IdRestaurant",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tourbookingDetails_Tourbookings_IdTourbookingDetails",
                        column: x => x.IdTourbookingDetails,
                        principalTable: "Tourbookings",
                        principalColumn: "IdTourbooking",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostTours_HotelId",
                table: "CostTours",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_CostTours_PlaceId",
                table: "CostTours",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CostTours_RestaurantId",
                table: "CostTours",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CarId",
                table: "Schedules",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                table: "Schedules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PromotionId",
                table: "Schedules",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TourId",
                table: "Schedules",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Timelines_IdSchedule",
                table: "Timelines",
                column: "IdSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_tourbookingDetails_HotelId",
                table: "tourbookingDetails",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_tourbookingDetails_PlaceId",
                table: "tourbookingDetails",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_tourbookingDetails_RestaurantId",
                table: "tourbookingDetails",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tourbookings_PaymentId",
                table: "Tourbookings",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tourbookings_ScheduleId",
                table: "Tourbookings",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TourDetails_TourId",
                table: "TourDetails",
                column: "TourId",
                unique: true,
                filter: "[TourId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictId",
                table: "Wards",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "CostTours");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Timelines");

            migrationBuilder.DropTable(
                name: "tourbookingDetails");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "TourDetails");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Tourbookings");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}