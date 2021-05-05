using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.EfConfigurations
{
    public class PatientEntityTypeConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.idPatient);
            builder.Property(e => e.idPatient).ValueGeneratedNever();
            builder.Property(e => e.firstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.lastName).IsRequired().HasMaxLength(150);
            builder.Property(e => e.birthdate).IsRequired().HasColumnType("datetime");
            //throw new NotImplementedException();
            builder.HasData(
                    new Patient { idPatient = 1, firstName = "Adam", lastName = "Kowalski", birthdate = new DateTime(2000,6,1,7,0,0) },
                    new Patient { idPatient = 2, firstName = "Janusz", lastName = "Małopolski", birthdate = new DateTime(2000, 6, 1, 7, 0, 0) }
                );
        }
    }
}
