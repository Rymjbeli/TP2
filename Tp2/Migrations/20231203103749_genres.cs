using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tp2.Migrations
{
    public partial class genres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "genres",
            columns: new[] { "Id", "Name" },
            values: new object[,]
        {
            { 1 , "Action" },
            { 2 , "Drama" },
            { 3 , "Comedy" },
            { 4 , "Science-Fiction"}
        });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
