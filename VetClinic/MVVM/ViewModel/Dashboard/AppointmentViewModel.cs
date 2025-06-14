using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class AppointmentViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;

        private DetailedAppointment _detailedAppointment;
        public DetailedAppointment DetailedAppointment
        {
            get => _detailedAppointment;
            set
            {
                if (value != null)
                {
                    _detailedAppointment = value;
                    PrescriptionFormViewModel = new PrescriptionFormViewModel(_detailedAppointment.Prescription, PrescriptionUpdated, _contextFactory);
                    ServicesFormViewModel = new ServicesFormViewModel(_detailedAppointment.Services, _contextFactory);
                    OnPropertyChanged();
                }
            }
        }


        private Prescription _prescription;
        public Prescription Prescription
        {
            get => _prescription;
            set
            {
                _prescription = value;
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

        private Service _selectedService;
        public Service SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged();
            }
        }

        private PrescriptionFormViewModel _prescriptionFormViewModel;
        public PrescriptionFormViewModel PrescriptionFormViewModel
        {
            get => _prescriptionFormViewModel;
            set
            {
                _prescriptionFormViewModel = value;
                OnPropertyChanged();
            }
        }

        private ServicesFormViewModel _servicesFormViewModel;
        public ServicesFormViewModel ServicesFormViewModel
        {
            get => _servicesFormViewModel;
            set
            {
                _servicesFormViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddPrescriptionCommand { get; }
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
        }

        private void OnPrescriptionUpdate(Prescription updatedPrescription)
        {
            if (updatedPrescription == null)
                return;

            Prescription = updatedPrescription;
        }

        private async Task CompleteAppointment(object obj)
        {
            using var context = _contextFactory.CreateDbContext();

            if (DetailedAppointment?.Appointment == null)
            {
                return;
            }
            DetailedAppointment.Appointment.Status = "Completed";

            context.Appointment.Update(DetailedAppointment.Appointment);

            // validate fielsd, prescription and services 
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

        private void Exit(object obj)
        {
            _exitAppointment?.Invoke();
        }
    }
}
