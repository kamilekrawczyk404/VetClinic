using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Admin
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public User User { get; set; }
    }
}
