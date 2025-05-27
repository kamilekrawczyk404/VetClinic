using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class AppointmentViewModel : ViewModel
    {
        private DetailedAppointment _appointment;
        public DetailedAppointment Appointment
        {
            get => _appointment;
            set
            {
                _appointment = value;
                OnPropertyChanged();
            }
        }

        RelayCommand AddPrescriptionCommand { get; }
        public AppointmentViewModel()
        {
            AddPrescriptionCommand = new RelayCommand(AddPrescription);
        }

        private void AddPrescription(object obj)
        {
            // Logic to add a prescription to the appointment
            // This could involve opening a dialog or navigating to a different view
            // For now, we will just simulate this with a console message
            //Console.WriteLine("Add Prescription command executed for appointment: " + Appointment?.Id);
        }
    }
}
