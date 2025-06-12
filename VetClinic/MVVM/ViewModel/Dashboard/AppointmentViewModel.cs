using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.MVVM.View.Window;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class AppointmentViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;

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


        private ObservableCollection<Prescription> _prescriptions;
        public ObservableCollection<Prescription> Prescriptions
        {
            get => _prescriptions;
            set
            {
                _prescriptions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Service> _services;
        public ObservableCollection<Service> Services
        {
            get => _services;
            set
            {
                _services = value;
                OnPropertyChanged();
            }
        }

        private Prescription _selectedPrescription;
        public Prescription SelectedPrescription 
        {
            get => _selectedPrescription;
            set
            {
                _selectedPrescription = value;
                OnPropertyChanged();
            }
        }

        private Prescription _selectedService;
        public Prescription SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddPrescriptionCommand { get; }
        public RelayCommand EditPrescriptionCommand { get; }
        public RelayCommand ExitAppointmentCommand { get; }
        public AsyncRelayCommand CompleteAppointmentCommand { get; }


        private Func<Task> _exitAppointment;

        private readonly Func<Task> _refreshAppointments;

        public event Action<Prescription> PrescriptionUpdated;
        public AppointmentViewModel(Func<Task> exitAppointment, Func<Task> refreshAppointments, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;

            _exitAppointment = exitAppointment;
            _refreshAppointments = refreshAppointments;

            PrescriptionUpdated += OnPrescriptionUpdate;

            AddPrescriptionCommand = new RelayCommand(AddPrescription);
            ExitAppointmentCommand = new RelayCommand(Exit);
            CompleteAppointmentCommand = new AsyncRelayCommand(CompleteAppointment);
            EditPrescriptionCommand = new RelayCommand(EditPrescription);
        }

        private void OnPrescriptionUpdate(Prescription updatedPrescription)
        {
            if (updatedPrescription == null)
                return;

            Prescription seeking = Prescriptions.FirstOrDefault(p => p.Id == updatedPrescription.Id);

            if (seeking == null)
            {
                Trace.WriteLine("Cannot find the prescription in the list");
                return;
            }

            Prescriptions[Prescriptions.IndexOf(seeking)] = updatedPrescription;

            SelectedPrescription = null;
        }

        private void EditPrescription(object obj)
        {
            if (SelectedPrescription != null)
            {
                var window = new PrescriptionDetailsWindow(SelectedPrescription, PrescriptionUpdated, _contextFactory);
                window.ShowDialog();
            }
        }

        private void Exit(object obj)
        {
            _exitAppointment?.Invoke();
        }

        private async Task CompleteAppointment(object obj)
        {
            using var context = _contextFactory.CreateDbContext();

            if (Appointment?.Appointment == null)
            {
                return;
            }
            Appointment.Appointment.AppointmentStatus = null;
            Appointment.Appointment.StatusId = 3; // 3 = completed

            context.Appointment.Update(Appointment.Appointment);
            await context.SaveChangesAsync();

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
