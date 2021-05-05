using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.EfConfigurations
{
    public class Prescription_MedicamentEntityTypeConfig : IEntityTypeConfiguration<Prescription_Medicament>
    {
        public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
        {
            builder.HasKey(e => e.idMedicament);
            builder.HasKey(e => e.idPrescription);
            builder.Property(e => e.dose);
            builder.Property(e => e.details).IsRequired().HasMaxLength(100);
            builder.HasOne(d => d.idPrescriptionNavigation).WithMany(p => p.prescriptionMedicaments).HasForeignKey(d => d.idPrescription).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(d => d.idMedicamentNavigation).WithMany(p => p.prescriptionMedicaments).HasForeignKey(d => d.idMedicament).OnDelete(DeleteBehavior.ClientSetNull);
            //throw new NotImplementedException();
            builder.HasData(
                    new Prescription_Medicament { idMedicament = 1, idPrescription = 1, dose = 3, details = "" },
                    new Prescription_Medicament { idMedicament = 2, idPrescription = 2, dose = 5, details = "" }
                );
        }
    }
}
