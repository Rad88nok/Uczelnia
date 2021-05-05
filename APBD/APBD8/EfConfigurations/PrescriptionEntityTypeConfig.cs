using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.EfConfigurations
{
    public class PrescriptionEntityTypeConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.idPrescription);
            builder.Property(e => e.idPrescription).ValueGeneratedNever();
            builder.Property(e => e.date).IsRequired().HasColumnType("datetime");
            builder.Property(e => e.dueDate).IsRequired().HasColumnType("datetime");
            builder.HasOne(d => d.idDoctorNavigation).WithMany(p => p.prescriptions).HasForeignKey(d => d.idDoctor).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(d => d.idPatientNavigation).WithMany(p => p.prescriptions).HasForeignKey(d => d.idPatient).OnDelete(DeleteBehavior.ClientSetNull);
            //throw new NotImplementedException();
            builder.HasData(
                    new Prescription { idPrescription = 1, date = DateTime.Now, dueDate = DateTime.Now, idPatient = 2, idDoctor = 2 },
                    new Prescription { idPrescription = 2, date = DateTime.Now, dueDate = DateTime.Now, idPatient = 1, idDoctor = 1 }
                );
        }
    }
}
