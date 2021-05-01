using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD3.Models
{
    public class Student
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string studentId { get; set; }
        public string birthDate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public string studyName { get; set; }
        public string studySemestrNumber { get; set; }

        public override string ToString()
        {
            return name + "," + surname + "," + studentId + "," + birthDate + "," +
                studyName + "," + studySemestrNumber + "," + email + "," +
                fathersName + "," + mothersName;
        }
    }
}
