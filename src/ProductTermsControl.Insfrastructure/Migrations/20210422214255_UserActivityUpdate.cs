using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class UserActivityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserActivities");

            migrationBuilder.AddColumn<string>(
                name: "ActivityType",
                table: "UserActivities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserActivities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "UserActivities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserActivities");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserActivities",
                type: "text",
                nullable: true);
        }
    }
}
