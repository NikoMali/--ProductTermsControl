using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class UpdateAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResponsiblePersonsByProducts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductToBranches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsiblePersonsGroupId",
                table: "ProductToBranches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ResponsiblePersonsForProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    ResponsiblePersonsGroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsiblePersonsForProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsiblePersonsForProducts_ResponsiblePersonsGroups_ResponsiblePersonsGroupId",
                        column: x => x.ResponsiblePersonsGroupId,
                        principalTable: "ResponsiblePersonsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsiblePersonsForProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductToBranches_ResponsiblePersonsGroupId",
                table: "ProductToBranches",
                column: "ResponsiblePersonsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePersonsForProducts_ResponsiblePersonsGroupId",
                table: "ResponsiblePersonsForProducts",
                column: "ResponsiblePersonsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePersonsForProducts_UserId",
                table: "ResponsiblePersonsForProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToBranches_ResponsiblePersonsGroups_ResponsiblePersonsGroupId",
                table: "ProductToBranches",
                column: "ResponsiblePersonsGroupId",
                principalTable: "ResponsiblePersonsGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductToBranches_ResponsiblePersonsGroups_ResponsiblePersonsGroupId",
                table: "ProductToBranches");

            migrationBuilder.DropTable(
                name: "ResponsiblePersonsForProducts");

            migrationBuilder.DropIndex(
                name: "IX_ProductToBranches_ResponsiblePersonsGroupId",
                table: "ProductToBranches");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductToBranches");

            migrationBuilder.DropColumn(
                name: "ResponsiblePersonsGroupId",
                table: "ProductToBranches");

            migrationBuilder.CreateTable(
                name: "ResponsiblePersonsByProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponsiblePersonsGroupId = table.Column<int>(type: "int", nullable: false),
                    TermDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsiblePersonsByProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsiblePersonsByProducts_ResponsiblePersonsGroups_ResponsiblePersonsGroupId",
                        column: x => x.ResponsiblePersonsGroupId,
                        principalTable: "ResponsiblePersonsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsiblePersonsByProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePersonsByProducts_ResponsiblePersonsGroupId",
                table: "ResponsiblePersonsByProducts",
                column: "ResponsiblePersonsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePersonsByProducts_UserId",
                table: "ResponsiblePersonsByProducts",
                column: "UserId");
        }
    }
}
