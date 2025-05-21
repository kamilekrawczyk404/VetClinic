using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public User User { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
