using APBD8.EfConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() { }
        public MainDbContext(DbContextOptions<MainDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Prescription> prescriptions { get; set; }
        public DbSet<Medicament> medicaments { get; set; }
        public DbSet<Prescription_Medicament> prescriptions_Medicaments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new DoctorEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new PrescriptionEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new MedicamentEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new Prescription_MedicamentEntityTypeConfig());
        }
    }
}
