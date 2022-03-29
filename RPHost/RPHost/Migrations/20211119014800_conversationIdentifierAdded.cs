using Microsoft.EntityFrameworkCore.Migrations;

namespace RPHost.Migrations
{
    public partial class conversationIdentifierAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConversationIdentifier",
                table: "Messages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationIdentifier",
                table: "Messages");
        }
    }
}
