using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.EfConfigurations
{
    public class DoctorEntityTypeConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.idDoctor);
            builder.Property(e => e.idDoctor).ValueGeneratedNever();
            builder.Property(e => e.firstName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.lastName).IsRequired().HasMaxLength(150);
            builder.Property(e => e.email).IsRequired().HasMaxLength(120);
            //throw new NotImplementedException();
            builder.HasData(
                    new Doctor { idDoctor = 1, firstName = "Jan", lastName = "Kowalski", email = "kowalski@gmail.com" },
                    new Doctor { idDoctor = 2, firstName = "Adam", lastName = "Małysz", email = "malysz@gmail.com" }
                );
        }
    }
}
