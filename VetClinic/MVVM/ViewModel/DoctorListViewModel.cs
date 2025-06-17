using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class DoctorListViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        public DoctorListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            Doctors = new ObservableCollection<Doctor>();

            AddDoctorCommand = new RelayCommand(AddDoctor, _ => IsAdmin);
            EditDoctorCommand = new RelayCommand(EditDoctor, _ => IsAdmin);
            DeleteDoctorCommand = new RelayCommand(DeleteDoctor, _ => IsAdmin);
            ViewOpinionsCommand = new RelayCommand(ViewOpinions);
            BookAppointmentCommand = new RelayCommand(BookAppointment, _ => IsClient);


            _userSessionService.UserChanged += async () => await OnUserChanged();
            _ = LoadDoctorsAsync();
        }

        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool IsClient => _userSessionService.IsClient;

        public RelayCommand AddDoctorCommand { get; }
        public RelayCommand EditDoctorCommand { get; }
        public RelayCommand DeleteDoctorCommand { get; }
        public RelayCommand AddOpinionCommand { get; }
        public RelayCommand ViewOpinionsCommand { get; }
        public RelayCommand BookAppointmentCommand { get; }


        private async Task OnUserChanged()
        {
            await LoadDoctorsAsync();
        }

        private async Task LoadDoctorsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                var doctors = await context.Doctor 
                    .OrderBy(d => d.Surname)
                    .ThenBy(d => d.Name)
                    .ToListAsync();

                Doctors = new ObservableCollection<Doctor>(doctors);

                Trace.WriteLine($"Loaded {doctors.Count} doctors from database");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch doctors: {ex.Message}");
                Trace.TraceError($"Stack trace: {ex.StackTrace}");
                Doctors = new ObservableCollection<Doctor>();
            }
        }

        private void AddDoctor(object obj)
        {
            if (!IsAdmin) return;

        }

        private void EditDoctor(object obj)
        {
            if (!(obj is Doctor doctor)) return;

        }

        private void DeleteDoctor(object obj)
        {
            if (!(obj is Doctor doctor)) return;

        }

        private void ViewOpinions(object obj)
        {
            if (!(obj is Doctor doctor))
            {
                return;
            }
            _navigationService.NavigateTo<ViewOpinionsViewModel>(doctor);
        }
        private void BookAppointment(object obj)
        {
            if (!(obj is Doctor doctor))
            {
                return;
            }
            _navigationService.NavigateTo<BookAppointmentViewModel>(doctor);
        }
        public async Task RefreshDoctorsAsync()
        {
            await LoadDoctorsAsync();
        }
    }
}