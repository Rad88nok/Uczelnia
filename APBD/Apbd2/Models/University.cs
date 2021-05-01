using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apbd2.Models
{
    class University
    {
        public University()
        {
        }

        public string date { get; set; }
        public string author { get; set; }
        public HashSet<Student> students { get; set; }




    }
}
