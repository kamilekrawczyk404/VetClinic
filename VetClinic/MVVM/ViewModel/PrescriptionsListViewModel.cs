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
    class PrescriptionListViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        public PrescriptionListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            ActivePrescriptions = new ObservableCollection<Prescription>();
            ExpiredPrescriptions = new ObservableCollection<Prescription>();

            _userSessionService.UserChanged += async () => await OnUserChanged();
            _ = LoadPrescriptionsAsync();
        }

        private ObservableCollection<Prescription> _activePrescriptions;
        public ObservableCollection<Prescription> ActivePrescriptions
        {
            get => _activePrescriptions;
            set
            {
                _activePrescriptions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Prescription> _expiredPrescriptions;
        public ObservableCollection<Prescription> ExpiredPrescriptions
        {
            get => _expiredPrescriptions;
            set
            {
                _expiredPrescriptions = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool IsClient => _userSessionService.IsClient;
        public bool IsDoctor => _userSessionService.IsDoctor;
        public bool IsAdmin => _userSessionService.IsAdmin;
        public int? CurrentUserId => _userSessionService.LoggedInUser?.Id;
        public int? CurrentDoctorId => _userSessionService.LoggedInDoctor?.Id;

        private async Task OnUserChanged()
        {
            await LoadPrescriptionsAsync();
        }

        private async Task LoadPrescriptionsAsync()
        {
            if (CurrentUserId == null && CurrentDoctorId == null)
            {
                ActivePrescriptions = new ObservableCollection<Prescription>();
                ExpiredPrescriptions = new ObservableCollection<Prescription>();
                return;
            }

            IsLoading = true;
            using var context = _contextFactory.CreateDbContext();

            try
            {
                var now = DateTime.Now;

                List<Prescription> allPrescriptions = new();

                if (IsClient)
                {
                    allPrescriptions = await context.Prescription
                    .Include(p => p.Appointment)
                        .ThenInclude(a => a.Pet)
                    .Include(p => p.Appointment)
                        .ThenInclude(a => a.Doctor)
                    .Include(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                    .Where(p => p.Appointment.Pet.UserId == CurrentUserId)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
                } else if (IsDoctor)
                {
                    allPrescriptions = await context.Prescription
                    .Include(p => p.Appointment)
                        .ThenInclude(a => a.Pet)
                    .Include(p => p.Appointment)
                        .ThenInclude(a => a.Doctor)
                    .Include(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                    .Where(p => p.Appointment.Doctor.Id == CurrentDoctorId)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
                } else if (IsAdmin)
                {
                    allPrescriptions = await context.Prescription
                    .Include(p => p.Appointment)
                        .ThenInclude(a => a.Pet)
                    .Include(p => p.Appointment)
                        .ThenInclude(a => a.Doctor)
                    .Include(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
                }

                var active = allPrescriptions
                .Where(p => p.ExpiryDate >= now)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

                var expired = allPrescriptions
                    .Where(p => p.ExpiryDate < now)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToList();

                ActivePrescriptions = new ObservableCollection<Prescription>(active);
                ExpiredPrescriptions = new ObservableCollection<Prescription>(expired);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch prescriptions: {ex.Message}");
                ActivePrescriptions = new ObservableCollection<Prescription>();
                ExpiredPrescriptions = new ObservableCollection<Prescription>();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}