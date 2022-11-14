using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Context.Migrations.Travel
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdAction",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdUserModify",
                table: "Promotions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTempdata",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifyBy",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifyDate",
                table: "Promotions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "TypeAction",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdAction",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "IdUserModify",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "IsTempdata",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "TypeAction",
                table: "Promotions");
        }
    }
}
