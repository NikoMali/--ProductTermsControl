using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class BranchStockUpdate1321qq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BranchProductStocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReasonForOutOfStockId",
                table: "BranchProductStocks",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReasonForOutOfStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForOutOfStocks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchProductStocks_ReasonForOutOfStockId",
                table: "BranchProductStocks",
                column: "ReasonForOutOfStockId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchProductStocks_ReasonForOutOfStocks_ReasonForOutOfStock~",
                table: "BranchProductStocks",
                column: "ReasonForOutOfStockId",
                principalTable: "ReasonForOutOfStocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchProductStocks_ReasonForOutOfStocks_ReasonForOutOfStock~",
                table: "BranchProductStocks");

            migrationBuilder.DropTable(
                name: "ReasonForOutOfStocks");

            migrationBuilder.DropIndex(
                name: "IX_BranchProductStocks_ReasonForOutOfStockId",
                table: "BranchProductStocks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BranchProductStocks");

            migrationBuilder.DropColumn(
                name: "ReasonForOutOfStockId",
                table: "BranchProductStocks");
        }
    }
}
