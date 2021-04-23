using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class updateAgainActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "UserActivities");

            migrationBuilder.AddColumn<string>(
                name: "RequestData",
                table: "UserActivities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestData",
                table: "UserActivities");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "UserActivities",
                type: "text",
                nullable: true);
        }
    }
}
