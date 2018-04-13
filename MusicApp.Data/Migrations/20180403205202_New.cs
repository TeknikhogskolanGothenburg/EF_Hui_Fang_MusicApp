using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.Data.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Concerts_ConcertId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_ConcertId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "ConcertId",
                table: "Times");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcertId",
                table: "Times",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Times_ConcertId",
                table: "Times",
                column: "ConcertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Concerts_ConcertId",
                table: "Times",
                column: "ConcertId",
                principalTable: "Concerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
