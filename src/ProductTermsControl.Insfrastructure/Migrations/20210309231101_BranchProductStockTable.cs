using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class BranchProductStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "BranchProductStocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    IsOutOfStock = table.Column<bool>(nullable: false),
                    OutOfStockReason = table.Column<string>(nullable: true),
                    ProductToBranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchProductStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchProductStocks_ProductToBranches_ProductToBranchId",
                        column: x => x.ProductToBranchId,
                        principalTable: "ProductToBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchProductStocks_ProductToBranchId",
                table: "BranchProductStocks",
                column: "ProductToBranchId");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchProductStocks");
        }
    }
}
