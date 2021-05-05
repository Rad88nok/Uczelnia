using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Models
{
    public class Prescription
    {
        public ICollection<Prescription_Medicament> prescriptionMedicaments { get; set; }
        public Prescription()
        {
            prescriptionMedicaments = new HashSet<Prescription_Medicament>();
        }

        public int idPrescription { get; set; }
        public DateTime date { get; set; }
        public DateTime dueDate { get; set; }
        public int idPatient { get; set; }
        public int idDoctor { get; set; }
        public Doctor idDoctorNavigation { get; set; }
        public Patient idPatientNavigation { get; set; }
    }
    
}
