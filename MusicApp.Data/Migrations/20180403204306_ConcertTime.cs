using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.Data.Migrations
{
    public partial class ConcertTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concerts_Times_TimeId",
                table: "Concerts");

            migrationBuilder.DropIndex(
                name: "IX_Concerts_TimeId",
                table: "Concerts");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Concerts");

            migrationBuilder.AddColumn<int>(
                name: "ConcertId",
                table: "Times",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConcertTime",
                columns: table => new
                {
                    ConcertId = table.Column<int>(nullable: false),
                    TimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertTime", x => new { x.ConcertId, x.TimeId });
                    table.ForeignKey(
                        name: "FK_ConcertTime_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcertTime_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Times_ConcertId",
                table: "Times",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertTime_TimeId",
                table: "ConcertTime",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Concerts_ConcertId",
                table: "Times",
                column: "ConcertId",
                principalTable: "Concerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Concerts_ConcertId",
                table: "Times");

            migrationBuilder.DropTable(
                name: "ConcertTime");

            migrationBuilder.DropIndex(
                name: "IX_Times_ConcertId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "ConcertId",
                table: "Times");

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "Concerts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Concerts_TimeId",
                table: "Concerts",
                column: "TimeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Concerts_Times_TimeId",
                table: "Concerts",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
