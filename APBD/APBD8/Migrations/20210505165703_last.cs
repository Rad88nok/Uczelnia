using Microsoft.EntityFrameworkCore.Migrations;

namespace APBD8.Migrations
{
    public partial class last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "doctors",
                columns: new[] { "idDoctor", "email", "firstName", "lastName" },
                values: new object[,]
                {
                    { 1, "kowalski@gmail.com", "Jan", "Kowalski" },
                    { 2, "malysz@gmail.com", "Adam", "Małysz" }
                });

            migrationBuilder.InsertData(
                table: "medicaments",
                columns: new[] { "idMedicament", "description", "name", "type" },
                values: new object[,]
                {
                    { 1, "", "Aspiryna", "" },
                    { 2, "", "Ibuprom", "" },
                    { 3, "", "Apap", "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "doctors",
                keyColumn: "idDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "doctors",
                keyColumn: "idDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "medicaments",
                keyColumn: "idMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "medicaments",
                keyColumn: "idMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "medicaments",
                keyColumn: "idMedicament",
                keyValue: 3);
        }
    }
}
