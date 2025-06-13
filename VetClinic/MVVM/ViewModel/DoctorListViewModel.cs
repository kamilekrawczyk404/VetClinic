using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public DoctorListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;

            Doctors = new ObservableCollection<Doctor>();

            AddDoctorCommand = new RelayCommand(AddDoctor, _ => IsAdmin);
            EditDoctorCommand = new RelayCommand(EditDoctor, _ => IsAdmin);
            DeleteDoctorCommand = new RelayCommand(DeleteDoctor, _ => IsAdmin);
            AddOpinionCommand = new RelayCommand(AddOpinion, _ => IsClient);
            ViewOpinionsCommand = new RelayCommand(ViewOpinions);

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
                    .Include(d => d.User)
                    .OrderBy(d => d.Surname)
                    .ThenBy(d => d.Name)
                    .ToListAsync();

                // Sprawdzamy czy klient już wystawił opinie dla każdego lekarza
                if (IsClient && _userSessionService.LoggedInUser != null)
                {
                    var clientOpinions = await context.Opinion
                        .Where(o => o.ClientId == _userSessionService.LoggedInUser.Id)
                        .Select(o => o.DoctorId)
                        .ToListAsync();

                    foreach (var doctor in doctors)
                    {
                        doctor.HasClientOpinion = clientOpinions.Contains(doctor.Id);
                    }
                }

                if (doctors.Count > 0)
                {
                    Doctors = new ObservableCollection<Doctor>(doctors);
                }
                else
                {
                    Doctors = new ObservableCollection<Doctor>();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceWarning($"Cannot fetch doctors: {ex.Message}");
                Doctors = new ObservableCollection<Doctor>();
            }
        }

        private void AddDoctor(object obj)
        {
            if (!IsAdmin) return;

            Trace.WriteLine("Opening Add Doctor window");
        }

        private void EditDoctor(object obj)
        {
            if (!(obj is Doctor doctor)) return;

        }

        private void DeleteDoctor(object obj)
        {
            if (!(obj is Doctor doctor)) return;

        }

        private void AddOpinion(object obj)
        {
            if (!(obj is Doctor doctor)) return;

        }

        private void ViewOpinions(object obj)
        {
            if (!(obj is Doctor doctor)) return;

        }

        public async Task RefreshDoctorsAsync()
        {
            await LoadDoctorsAsync();
        }
    }
}