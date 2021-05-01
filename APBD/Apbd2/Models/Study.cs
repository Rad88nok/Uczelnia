using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apbd2.Models
{
    public class Study
    {
        public Study(string name, string mode) {this.name = name; this.mode = mode;
        }

        public string name { get; set; }
        public string mode { get; set; }
    }
}
