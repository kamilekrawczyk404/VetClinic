using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Prescription
    {
        public int id { get; set; }
        public int appointment_id { get; set; }
        public string instructions { get; set; }
        public DateTime expiry_date { get; set; }
        public DateTime created_at { get; set; }

        public Appointment Appointment { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<PrescriptionDrugs> PrescriptionDrugs { get; set; }
    }
}
