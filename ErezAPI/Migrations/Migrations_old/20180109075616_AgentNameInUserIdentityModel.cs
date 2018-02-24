using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ErezAPI.Migrations
{
    public partial class AgentNameInUserIdentityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isTodays",
                table: "EnrollActivities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AgentName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTodays",
                table: "EnrollActivities");

            migrationBuilder.DropColumn(
                name: "AgentName",
                table: "AspNetUsers");
        }
    }
}
