using APBD8.DTO.Response;
using APBD8.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Services
{
    public class PrescriptionServise : IPrescriptionServise
    {
        public async Task<GetPrescriptionInfoResponseDto> GetPrescription(MainDbContext context, string index)
        {
            int idPrescription = int.Parse(index);
            var prescription = await context.prescriptions
                                            .Where(e => e.idPrescription == int.Parse(index))
                                            .Include(e => e.idDoctorNavigation)
                                            .Include(e => e.idPatientNavigation)
                                            .Include(e => e.prescriptionMedicaments)
                                            .ThenInclude(e => e.idMedicamentNavigation)
                                            .Select(e => new GetPrescriptionInfoResponseDto
                                            {
                                                date = e.date,
                                                dueDate = e.dueDate,
                                                patient = e.idPatientNavigation.firstName + " " + e.idPatientNavigation.lastName,
                                                doctor = e.idDoctorNavigation.firstName + " " + e.idDoctorNavigation.lastName,
                                                Medicaments = e.prescriptionMedicaments.Select(e => new GetMedicamentInfoResponseDto
                                                {
                                                    name = e.idMedicamentNavigation.name,
                                                    description = e.idMedicamentNavigation.description,
                                                    type = e.idMedicamentNavigation.type
                                                })
                                            })
                                            .FirstOrDefaultAsync();
            return prescription;
        }
    }
}
