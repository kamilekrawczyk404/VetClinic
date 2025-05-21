using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class AppointmentServices
    {
        public int id { get; set; }
        public int appointment_id { get; set; }
        public int service_id { get; set; }

        public Appointment Appointment { get; set; }
        public Service Service { get; set; }
    }
}
