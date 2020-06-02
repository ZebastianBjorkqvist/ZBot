using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SummonerModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SummonerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankHistoryModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    Rank = table.Column<string>(nullable: true),
                    SummonerModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankHistoryModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RankHistoryModels_SummonerModels_SummonerModelId",
                        column: x => x.SummonerModelId,
                        principalTable: "SummonerModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RankHistoryModels_SummonerModelId",
                table: "RankHistoryModels",
                column: "SummonerModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RankHistoryModels");

            migrationBuilder.DropTable(
                name: "SummonerModels");
        }
    }
}
