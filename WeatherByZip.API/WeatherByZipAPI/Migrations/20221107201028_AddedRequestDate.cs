using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherByZip.API.Migrations
{
    public partial class AddedRequestDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "CityInfos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "CityInfos");
        }
    }
}
