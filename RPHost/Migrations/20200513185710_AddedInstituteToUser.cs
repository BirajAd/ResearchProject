<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore.Migrations;

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
=======
﻿using Microsoft.EntityFrameworkCore.Migrations;

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
>>>>>>> ed1991926c2c567cff1e6766160db2069e04f9f1
