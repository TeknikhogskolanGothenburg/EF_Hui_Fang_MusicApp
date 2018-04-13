using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicApp.Data.Migrations
{
    public partial class addStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"CREATE PROCEDURE FilterConcertByTitlePart
                @titlepart varchar(50)
                AS
                SELECT * FROM Concerts WHERE Name LIKE '%' +@titlepart + '%'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE FilterConcertByTitlePart");
        }
    }
}
