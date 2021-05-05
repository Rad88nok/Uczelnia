using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APBD8.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    idDoctor = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.idDoctor);
                });

            migrationBuilder.CreateTable(
                name: "medicaments",
                columns: table => new
                {
                    idMedicament = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicaments", x => x.idMedicament);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    idPatient = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.idPatient);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    idPrescription = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    idPatient = table.Column<int>(type: "int", nullable: false),
                    idDoctor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.idPrescription);
                    table.ForeignKey(
                        name: "FK_prescriptions_doctors_idDoctor",
                        column: x => x.idDoctor,
                        principalTable: "doctors",
                        principalColumn: "idDoctor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prescriptions_patients_idPatient",
                        column: x => x.idPatient,
                        principalTable: "patients",
                        principalColumn: "idPatient",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions_Medicaments",
                columns: table => new
                {
                    idPrescription = table.Column<int>(type: "int", nullable: false),
                    idMedicament = table.Column<int>(type: "int", nullable: false),
                    dose = table.Column<int>(type: "int", nullable: true),
                    details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions_Medicaments", x => x.idPrescription);
                    table.ForeignKey(
                        name: "FK_prescriptions_Medicaments_medicaments_idMedicament",
                        column: x => x.idMedicament,
                        principalTable: "medicaments",
                        principalColumn: "idMedicament",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prescriptions_Medicaments_prescriptions_idPrescription",
                        column: x => x.idPrescription,
                        principalTable: "prescriptions",
                        principalColumn: "idPrescription",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_idDoctor",
                table: "prescriptions",
                column: "idDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_idPatient",
                table: "prescriptions",
                column: "idPatient");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_Medicaments_idMedicament",
                table: "prescriptions_Medicaments",
                column: "idMedicament");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prescriptions_Medicaments");

            migrationBuilder.DropTable(
                name: "medicaments");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
