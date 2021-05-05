using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.EfConfigurations
{
    public class MedicamentEntityTypeConfig :IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(e => e.idMedicament);
            builder.Property(e => e.idMedicament).ValueGeneratedNever();
            builder.Property(e => e.name).IsRequired().HasMaxLength(200);
            builder.Property(e => e.description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.type).IsRequired().HasMaxLength(100);
            //throw new NotImplementedException();
            builder.HasData(
                    new Medicament { idMedicament = 1, name = "Aspiryna", description = "", type = "" },
                    new Medicament { idMedicament = 2, name = "Ibuprom", description = "", type = "" },
                    new Medicament { idMedicament = 3, name = "Apap", description = "", type = "" }
                    );
        }
    }
}
