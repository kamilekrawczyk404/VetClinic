using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Pet
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public string name { get; set; }
        public string species { get; set; }
        public string breed { get; set; }
        public double weight { get; set; }
        public string gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public DateTime created_at { get; set; }

        public Client Client { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
