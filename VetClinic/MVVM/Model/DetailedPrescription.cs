using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.MVVM.Model
{
    public class DetailedPrescription
    {
        public Appointment Appointment { get; set; }
        public User Client { get; set; }
        public Doctor Doctor { get; set; }
    }
}
