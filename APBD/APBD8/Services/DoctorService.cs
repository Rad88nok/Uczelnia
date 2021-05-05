using APBD8.DTO.Request;
using APBD8.DTO.Response;
using APBD8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Services
{
    public class DoctorService : IDoctorService
    {
        public async Task DeleteDoctor(MainDbContext context, string index)
        {
            if (!await DoctorExists(int.Parse(index)))
            {
                throw new ArgumentException("Doctor with ID " + index + " doesnt exists");
            }

            var doctor = new Doctor
            {
                idDoctor = int.Parse(index)
            };
            var prescriptions = await GetPrescriptionsWithIdDoctor(int.Parse(index));

            foreach (var p in prescriptions)
            {
                var pres_med = await GetPrescriptions_MedicamentWithIdPrescription(p.idPrescription);
                context.prescriptions.Remove(p);
                context.prescriptions_Medicaments.Remove(pres_med);
            }
            context.doctors.Attach(doctor);
            context.Entry(doctor).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
        private async Task<IEnumerable<Prescription>> GetPrescriptionsWithIdDoctor(int doctorIndex)
        {
            var context = new MainDbContext();
            var prescriptions = await context.prescriptions
                                             .Where(e => e.idDoctor == doctorIndex)
                                             .ToListAsync();
            return prescriptions;
        }
        private async Task<Prescription_Medicament> GetPrescriptions_MedicamentWithIdPrescription(int prescriptionIndex)
        {
            var context = new MainDbContext();
            var prescription_medicament = await context.prescriptions_Medicaments
                                             .Where(e => e.idPrescription == prescriptionIndex)
                                             .FirstAsync();
            return prescription_medicament;
        }

        public async Task<IEnumerable<GetDoctorInfoResponseDto>> GetDoctors(MainDbContext context)
        {
            var doctors = await context.doctors.Include(e => e.prescriptions).ThenInclude(e => e.idPatientNavigation)
                                         .Select(e => new GetDoctorInfoResponseDto
                                         {
                                             firstName = e.firstName,
                                             lastName = e.lastName,
                                             email = e.email,
                                             Patients = e.prescriptions.Select(x => new GetPatientInfoResponseDto
                                             {
                                                 firstName = x.idPatientNavigation.firstName,
                                                 lastName = x.idPatientNavigation.lastName
                                             })
                                         })
                                         .ToListAsync();
            return doctors;
            //throw new NotImplementedException();
        }

        public async Task PostDoctor(MainDbContext context, GetDoctorInfoRequestDto doctorInput)
        {
            var doctor = new Doctor
            {
                idDoctor = context.doctors.Max(e => e.idDoctor) + 1,
                firstName = doctorInput.firstName,
                lastName = doctorInput.lastName,
                email = doctorInput.email
            };
            context.doctors.Add(doctor);
            await context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task PutDoctor(MainDbContext context, GetDoctorInfoRequestDto doctorInput, string index)
        {
            if (!await DoctorExists(int.Parse(index)))
            {
                throw new ArgumentException("Doctor with ID " + index + " doesnt exists");
            }
            var doctor = new Doctor
            {
                idDoctor = int.Parse(index),
                firstName = doctorInput.firstName,
                lastName = doctorInput.lastName,
                email = doctorInput.email
            };
            context.doctors.Attach(doctor);
            context.Entry(doctor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
        private async Task<bool> DoctorExists(int idDoctor)
        {
            var context = new MainDbContext();
            var result = await context.doctors
                .Where(t => t.idDoctor == idDoctor)
                .ToListAsync();

            return result.Count > 0;
        }
    }
}
