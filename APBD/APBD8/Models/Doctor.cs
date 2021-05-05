using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD8.Models
{
    public class Doctor
    {
        public ICollection<Prescription> prescriptions { get; set; }
        public Doctor()
        {
            prescriptions = new HashSet<Prescription>();
        }

        public int idDoctor { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
    }
}
