using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Controls;
using VetClinic.Models;

namespace VetClinic.MVVM.Model
{
    public class DetailedAppointment
    {
        public Appointment Appointment { get; set; }
        public Pet Pet { get; set; }
        public Doctor Doctor { get; set; }
        public User Client {get; set;}
        public int Duration { get; set; } = 30; // Default duration in minutes
        public Prescription Prescription { get; set; } = new();
        public ObservableCollection<string> Statuses { get; set; } = new();
        public ObservableCollection<Service> Services { get; set; } = new();
    }
}
