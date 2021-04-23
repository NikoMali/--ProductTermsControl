using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductTermsControl.Insfrastructure.Migrations
{
    public partial class updateActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "response",
                table: "UserActivities",
                type: "LONGTEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserActivities",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserActivities_Users_UserId",
                table: "UserActivities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserActivities_Users_UserId",
                table: "UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_UserActivities_UserId",
                table: "UserActivities");

            migrationBuilder.AlterColumn<string>(
                name: "response",
                table: "UserActivities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "LONGTEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserActivities",
                type: "text",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
