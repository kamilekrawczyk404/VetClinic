using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Service
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<AppointmentServices> AppointmentServices { get; set; }
    }
}
