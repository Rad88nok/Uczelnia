using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Models
{
    public class Patient
    {
        public ICollection<Prescription> prescriptions { get; set; }
        public Patient()
        {
            prescriptions = new HashSet<Prescription>();
        }

        public int idPatient { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthdate { get; set; }
    }
}
