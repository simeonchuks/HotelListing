using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListing.Api.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "391518e8-ac9e-4e98-bd39-00be875f353c", "1ce9f649-9acf-43ee-8a8a-51a8dc8db5ab", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2afea069-6991-40f9-8de2-224e16804ba6", "a967501b-0a98-4470-bb2d-f1ed6ca552a6", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2afea069-6991-40f9-8de2-224e16804ba6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "391518e8-ac9e-4e98-bd39-00be875f353c");
        }
    }
}
