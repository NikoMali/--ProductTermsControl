using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class BaseEntityi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ResponsiblePersonsForProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "ResponsiblePersonsForProducts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductToBranches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "ProductToBranches",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ResponsiblePersonsForProducts");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "ResponsiblePersonsForProducts");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductToBranches");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "ProductToBranches");
        }
    }
}
