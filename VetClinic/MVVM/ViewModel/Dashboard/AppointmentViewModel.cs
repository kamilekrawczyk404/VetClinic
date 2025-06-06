using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class AppointmentViewModel : ViewModel
    {
        private readonly VeterinaryClinicContext _database;

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

        public RelayCommand AddPrescriptionCommand { get; }
        public RelayCommand ExitAppointmentCommand { get; }
        public AsyncRelayCommand CompleteAppointmentCommand { get; }

        private Func<Task> _exitAppointment;

        private readonly Func<Task> _refreshAppointments;
        public AppointmentViewModel(VeterinaryClinicContext database, Func<Task> exitAppointment, Func<Task> refreshAppointments)
        {
            _database = database;

            _exitAppointment = exitAppointment;
            _refreshAppointments = refreshAppointments;

            AddPrescriptionCommand = new RelayCommand(AddPrescription);
            ExitAppointmentCommand = new RelayCommand(Exit);
            CompleteAppointmentCommand = new AsyncRelayCommand(CompleteAppointment);
        }

        private void Exit(object obj)
        {
            _exitAppointment?.Invoke();
        }

        private async Task CompleteAppointment(object obj)
        {
            if (Appointment?.Appointment == null)
            {
                return;
            }
            Appointment.Appointment.StatusId = 3; // 3 = completed

            _database.Appointment.Update(Appointment.Appointment);
            await _database.SaveChangesAsync();

            _refreshAppointments?.Invoke();
            _exitAppointment?.Invoke();
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
