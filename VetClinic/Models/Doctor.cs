using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Doctor
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string specialization { get; set; }
        public string? description { get; set; }

        public User User { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Opinion> Opinions { get; set; }
    }
}
