using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Appointment
    {
        public int id { get; set; }
        public int pet_id { get; set; }
        public int doctor_id { get; set; }
        public DateTime appointment_date { get; set; }
        public string reason_for_visit { get; set; }
        public string notes { get; set; }
        public string diagnosis { get; set; }
        public DateTime created_at { get; set; }

        public Pet Pet { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<AppointmentServices> AppointmentServices { get; set; }
    }
}
