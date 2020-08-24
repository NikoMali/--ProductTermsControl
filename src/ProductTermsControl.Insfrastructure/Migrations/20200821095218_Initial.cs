using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Magazines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResponsiblePersonsGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsiblePersonsGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IdentificationCode = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MagazineBranches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    MagazineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MagazineBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MagazineBranches_Magazines_MagazineId",
                        column: x => x.MagazineId,
                        principalTable: "Magazines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductToBranches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: false),
                    DaysBeforeNotifiWarning = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    MagazineBranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductToBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductToBranches_MagazineBranches_MagazineBranchId",
                        column: x => x.MagazineBranchId,
                        principalTable: "MagazineBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductToBranches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    MagazineBranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_MagazineBranches_MagazineBranchId",
                        column: x => x.MagazineBranchId,
                        principalTable: "MagazineBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsiblePersonsByProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    TermDate = table.Column<DateTime>(nullable: false),
                    ResponsiblePersonsGroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
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
                name: "IX_MagazineBranches_MagazineId",
                table: "MagazineBranches",
                column: "MagazineId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToBranches_MagazineBranchId",
                table: "ProductToBranches",
                column: "MagazineBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToBranches_ProductId",
                table: "ProductToBranches",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePersonsByProducts_ResponsiblePersonsGroupId",
                table: "ResponsiblePersonsByProducts",
                column: "ResponsiblePersonsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePersonsByProducts_UserId",
                table: "ResponsiblePersonsByProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MagazineBranchId",
                table: "Users",
                column: "MagazineBranchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductToBranches");

            migrationBuilder.DropTable(
                name: "ResponsiblePersonsByProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ResponsiblePersonsGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropTable(
                name: "MagazineBranches");

            migrationBuilder.DropTable(
                name: "Magazines");
        }
    }
}
