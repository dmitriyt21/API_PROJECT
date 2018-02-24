using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ErezAPI.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrollActivities",
                columns: table => new
                {
                    EnrollActionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentId = table.Column<int>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 50, nullable: true),
                    AgentPlace = table.Column<int>(nullable: false),
                    PercOfTarget = table.Column<double>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    SumOfSales = table.Column<decimal>(nullable: false),
                    Target = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollActivities", x => x.EnrollActionId);
                });

            migrationBuilder.CreateTable(
                name: "OverallActivities",
                columns: table => new
                {
                    EnrollActionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentId = table.Column<int>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 50, nullable: true),
                    AgentPlace = table.Column<int>(nullable: false),
                    EnrollPoints = table.Column<decimal>(nullable: false),
                    OverallPoints = table.Column<int>(nullable: false),
                    RetentionPoints = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverallActivities", x => x.EnrollActionId);
                });

            migrationBuilder.CreateTable(
                name: "RetentionActivities",
                columns: table => new
                {
                    RetentionActionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentId = table.Column<int>(nullable: false),
                    AgentName = table.Column<string>(maxLength: 50, nullable: true),
                    AgentPlace = table.Column<int>(nullable: false),
                    NumOfExistCust = table.Column<int>(nullable: false),
                    NumOfNewCust = table.Column<int>(nullable: false),
                    PercOfTarget = table.Column<double>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Target = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetentionActivities", x => x.RetentionActionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollActivities");

            migrationBuilder.DropTable(
                name: "OverallActivities");

            migrationBuilder.DropTable(
                name: "RetentionActivities");
        }
    }
}
