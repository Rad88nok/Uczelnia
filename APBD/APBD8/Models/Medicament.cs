using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Models
{
    public class Medicament
    {
        public ICollection<Prescription_Medicament> prescriptionMedicaments { get; set; }
        public Medicament()
        {
            prescriptionMedicaments = new HashSet<Prescription_Medicament>();
        }
        public int idMedicament { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

    }
}
