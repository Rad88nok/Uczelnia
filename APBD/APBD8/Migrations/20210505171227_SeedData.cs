using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APBD8.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "patients",
                columns: new[] { "idPatient", "birthdate", "firstName", "lastName" },
                values: new object[] { 1, new DateTime(2000, 6, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Kowalski" });

            migrationBuilder.InsertData(
                table: "patients",
                columns: new[] { "idPatient", "birthdate", "firstName", "lastName" },
                values: new object[] { 2, new DateTime(2000, 6, 1, 7, 0, 0, 0, DateTimeKind.Unspecified), "Janusz", "Małopolski" });

            migrationBuilder.InsertData(
                table: "prescriptions",
                columns: new[] { "idPrescription", "date", "dueDate", "idDoctor", "idPatient" },
                values: new object[] { 2, new DateTime(2021, 5, 5, 19, 12, 26, 329, DateTimeKind.Local).AddTicks(6532), new DateTime(2021, 5, 5, 19, 12, 26, 329, DateTimeKind.Local).AddTicks(6558), 1, 1 });

            migrationBuilder.InsertData(
                table: "prescriptions",
                columns: new[] { "idPrescription", "date", "dueDate", "idDoctor", "idPatient" },
                values: new object[] { 1, new DateTime(2021, 5, 5, 19, 12, 26, 323, DateTimeKind.Local).AddTicks(1257), new DateTime(2021, 5, 5, 19, 12, 26, 329, DateTimeKind.Local).AddTicks(3820), 2, 2 });

            migrationBuilder.InsertData(
                table: "prescriptions_Medicaments",
                columns: new[] { "idPrescription", "details", "dose", "idMedicament" },
                values: new object[] { 2, "", 5, 2 });

            migrationBuilder.InsertData(
                table: "prescriptions_Medicaments",
                columns: new[] { "idPrescription", "details", "dose", "idMedicament" },
                values: new object[] { 1, "", 3, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "prescriptions_Medicaments",
                keyColumn: "idPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "prescriptions_Medicaments",
                keyColumn: "idPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "prescriptions",
                keyColumn: "idPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "prescriptions",
                keyColumn: "idPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "patients",
                keyColumn: "idPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "patients",
                keyColumn: "idPatient",
                keyValue: 2);
        }
    }
}
