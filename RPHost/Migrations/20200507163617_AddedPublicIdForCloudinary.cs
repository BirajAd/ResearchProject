<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RPHost.Migrations
{
    public partial class AddedPublicIdForCloudinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photos");
        }
    }
}
=======
﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RPHost.Migrations
{
    public partial class AddedPublicIdForCloudinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Photos");
        }
    }
}
>>>>>>> ed1991926c2c567cff1e6766160db2069e04f9f1
