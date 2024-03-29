﻿// <auto-generated />
using System;
using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APBD8.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20210505164325_final")]
    partial class final
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APBD8.Models.Doctor", b =>
                {
                    b.Property<int>("idDoctor")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("idDoctor");

                    b.ToTable("doctors");
                });

            modelBuilder.Entity("APBD8.Models.Medicament", b =>
                {
                    b.Property<int>("idMedicament")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("idMedicament");

                    b.ToTable("medicaments");
                });

            modelBuilder.Entity("APBD8.Models.Patient", b =>
                {
                    b.Property<int>("idPatient")
                        .HasColumnType("int");

                    b.Property<DateTime>("birthdate")
                        .HasColumnType("datetime");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("idPatient");

                    b.ToTable("patients");
                });

            modelBuilder.Entity("APBD8.Models.Prescription", b =>
                {
                    b.Property<int>("idPrescription")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("dueDate")
                        .HasColumnType("datetime");

                    b.Property<int>("idDoctor")
                        .HasColumnType("int");

                    b.Property<int>("idPatient")
                        .HasColumnType("int");

                    b.HasKey("idPrescription");

                    b.HasIndex("idDoctor");

                    b.HasIndex("idPatient");

                    b.ToTable("prescriptions");
                });

            modelBuilder.Entity("APBD8.Models.Prescription_Medicament", b =>
                {
                    b.Property<int>("idPrescription")
                        .HasColumnType("int");

                    b.Property<string>("details")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("dose")
                        .HasColumnType("int");

                    b.Property<int>("idMedicament")
                        .HasColumnType("int");

                    b.HasKey("idPrescription");

                    b.HasIndex("idMedicament");

                    b.ToTable("prescriptions_Medicaments");
                });

            modelBuilder.Entity("APBD8.Models.Prescription", b =>
                {
                    b.HasOne("APBD8.Models.Doctor", "idDoctorNavigation")
                        .WithMany("prescriptions")
                        .HasForeignKey("idDoctor")
                        .IsRequired();

                    b.HasOne("APBD8.Models.Patient", "idPatientNavigation")
                        .WithMany("prescriptions")
                        .HasForeignKey("idPatient")
                        .IsRequired();

                    b.Navigation("idDoctorNavigation");

                    b.Navigation("idPatientNavigation");
                });

            modelBuilder.Entity("APBD8.Models.Prescription_Medicament", b =>
                {
                    b.HasOne("APBD8.Models.Medicament", "idMedicamentNavigation")
                        .WithMany("prescriptionMedicaments")
                        .HasForeignKey("idMedicament")
                        .IsRequired();

                    b.HasOne("APBD8.Models.Prescription", "idPrescriptionNavigation")
                        .WithMany("prescriptionMedicaments")
                        .HasForeignKey("idPrescription")
                        .IsRequired();

                    b.Navigation("idMedicamentNavigation");

                    b.Navigation("idPrescriptionNavigation");
                });

            modelBuilder.Entity("APBD8.Models.Doctor", b =>
                {
                    b.Navigation("prescriptions");
                });

            modelBuilder.Entity("APBD8.Models.Medicament", b =>
                {
                    b.Navigation("prescriptionMedicaments");
                });

            modelBuilder.Entity("APBD8.Models.Patient", b =>
                {
                    b.Navigation("prescriptions");
                });

            modelBuilder.Entity("APBD8.Models.Prescription", b =>
                {
                    b.Navigation("prescriptionMedicaments");
                });
#pragma warning restore 612, 618
        }
    }
}
