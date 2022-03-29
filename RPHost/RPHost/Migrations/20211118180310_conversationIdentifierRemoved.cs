using Microsoft.EntityFrameworkCore.Migrations;

namespace RPHost.Migrations
{
    public partial class conversationIdentifierRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationIdentifier",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationIdentifier",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConversationIdentifier",
                table: "Messages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationIdentifier",
                table: "Messages",
                column: "ConversationIdentifier");
        }
    }
}
