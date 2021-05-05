using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Models
{
    public class Prescription_Medicament
    {
        public Medicament idMedicamentNavigation { get; set; }
        public Prescription idPrescriptionNavigation { get; set; }
        public int idMedicament { get; set; }
        public int idPrescription { get; set; }
        public int? dose { get; set; }
        public string details { get; set; }
    }
}
