using Microsoft.EntityFrameworkCore.Migrations;

namespace RPHost.Migrations
{
    public partial class AddedInstituteToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Institute",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Institute",
                table: "Users");
        }
    }
}
