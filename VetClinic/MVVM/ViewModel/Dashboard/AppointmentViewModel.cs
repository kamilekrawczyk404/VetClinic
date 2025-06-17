using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{

    // TODO: cannot edit presciptions and services list if it's completed or cancelled 
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

                    OnPropertyChanged();
                    OnDetailedAppointmentChanged();
                }
            }
        }

        private void OnDetailedAppointmentChanged()
        {
            PrescriptionFormViewModel = new PrescriptionFormViewModel(_detailedAppointment.Prescription, new()
            {
                Appointment = _detailedAppointment.Appointment,
                Client = _detailedAppointment.Appointment.Pet.User,
                Doctor = _detailedAppointment.Appointment.Doctor,
            }, _contextFactory);

            ServicesFormViewModel = new ServicesFormViewModel(_detailedAppointment.Services, _contextFactory);

            IsCompleted = _detailedAppointment.Appointment.Status == "Completed" || _detailedAppointment.Appointment.Status == "Cancelled";
            Diagnosis = _detailedAppointment.Appointment.Diagnosis;
            Notes = _detailedAppointment?.Appointment.Notes ?? "";

            PrescriptionFormViewModel.IsAppointmentCompleted = IsCompleted;
            PrescriptionFormViewModel.HasPrescription = _detailedAppointment?.Prescription?.Id != null;
            PrescriptionFormViewModel.IsPrescriptionDisplayed = IsCompleted && !PrescriptionFormViewModel.HasPrescription;
            ServicesFormViewModel.IsAppointmentCompleted = IsCompleted;
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged();
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

        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        private string _diagnosis;
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value;
                OnPropertyChanged();
            }
        }

        private string _diagnosisErrorMessage;
        public string DiagnosisErrorMessage
        {
            get => _diagnosisErrorMessage;
            set
            {
                _diagnosisErrorMessage = value;
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

        public RelayCommand ExitAppointmentCommand { get; }
        public AsyncRelayCommand SaveAppointmentCommand { get; }


        private Func<Task> _exitAppointment;

        private readonly Func<int, Task> _refreshAppointments;

        public AppointmentViewModel(Func<Task> exitAppointment, Func<int, Task> refreshAppointments, IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;

            _exitAppointment = exitAppointment;
            _refreshAppointments = refreshAppointments;

            ExitAppointmentCommand = new RelayCommand(Exit);
            SaveAppointmentCommand = new AsyncRelayCommand(SaveAppointment);

            DiagnosisErrorMessage  = string.Empty;
        }

        private async Task SaveAppointment(object obj)
        {
            if (DetailedAppointment?.Appointment == null)
            {
                return;
            }

            DiagnosisErrorMessage = string.Empty;

            if (string.IsNullOrEmpty(Diagnosis))
            {
                DiagnosisErrorMessage = "| Cannot be empty";
                return;
            }

            using var context = _contextFactory.CreateDbContext();

            bool hasChanged = false;

            if (DetailedAppointment.Appointment.Diagnosis != Diagnosis)
            {
                DetailedAppointment.Appointment.Diagnosis = Diagnosis;
                hasChanged = true;
            }

            if (DetailedAppointment.Appointment.Notes != Notes)
            {
                DetailedAppointment.Appointment.Notes = Notes != null ? Notes : "";
                hasChanged = true;
            }

            DetailedAppointment.Appointment.Status = "Completed";

            bool areServicesValid = await ServicesFormViewModel.CheckServices(DetailedAppointment.Appointment);

            Trace.WriteLine("VALIDATION Services " + areServicesValid.ToString());
            if (!areServicesValid)
                return;

            bool isPrescriptionValid = await PrescriptionFormViewModel.CheckPrescription();

            Trace.WriteLine("VALIDATION Prescription " + isPrescriptionValid.ToString());


            if (!isPrescriptionValid)
                return;

            // validation of services and prescription passed successfully, update records
            context.Appointment.Update(DetailedAppointment.Appointment);
            await context.SaveChangesAsync();

            _refreshAppointments?.Invoke(DetailedAppointment.Appointment.Id);
            _exitAppointment?.Invoke();
        }

        private void Exit(object obj)
        {
            _exitAppointment?.Invoke();
        }
    }
}
