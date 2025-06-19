using Microsoft.EntityFrameworkCore;
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
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class AdminDashboardViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;

        private User _admin;

        private bool _isAppointmentDisplayed;
        public bool IsAppointmentDisplayed
        {
            get => _isAppointmentDisplayed;
            set
            {
                _isAppointmentDisplayed = value;
                OnPropertyChanged();
            }
        }

        #region Dashboard Statistics Properties

        private int _appointmentsCount;
        public int AppointmentsCount
        {
            get => _appointmentsCount;
            set
            {
                _appointmentsCount = value;
                OnPropertyChanged();
            }
        }

        private double _appointmentsRatio;
        public double AppointmentsRatio
        {
            get => _appointmentsRatio;
            set
            {
                _appointmentsRatio = value;
                OnPropertyChanged();
            }
        }

        private int _clientsCount;
        public int ClientsCount
        {
            get => _clientsCount;
            set
            {
                _clientsCount = value;
                OnPropertyChanged();
            }
        }

        private double _clientsRatio;
        public double ClientsRatio
        {
            get => _clientsRatio;
            set
            {
                _clientsRatio = value;
                OnPropertyChanged();
            }
        }

        private int _petsCount;
        public int PetsCount
        {
            get => _petsCount;
            set
            {
                _petsCount = value;
                OnPropertyChanged();
            }
        }

        private double _petsRatio;
        public double PetsRatio
        {
            get => _petsRatio;
            set
            {
                _petsRatio = value;
                OnPropertyChanged();
            }
        }

        private decimal _revenueAmount;
        public decimal RevenueAmount
        {
            get => _revenueAmount;
            set
            {
                _revenueAmount = value;
                OnPropertyChanged();
            }
        }

        private double _revenueRatio;
        public double RevenueRatio
        {
            get => _revenueRatio;
            set
            {
                _revenueRatio = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailedAppointment> _allAppointments = new ObservableCollection<DetailedAppointment>();
        public ObservableCollection<DetailedAppointment> AllAppointments
        {
            get => _allAppointments;
            set
            {
                _allAppointments = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailedAppointment> _selectedDayAppointments = new ObservableCollection<DetailedAppointment>();
        public ObservableCollection<DetailedAppointment> SelectedDayAppointments
        {
            get => _selectedDayAppointments;
            set
            {
                _selectedDayAppointments = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CalendarDay> _calendarDays = new ObservableCollection<CalendarDay>();
        public ObservableCollection<CalendarDay> CalendarDays
        {
            get => _calendarDays;
            set
            {
                _calendarDays = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _statuses;
        public ObservableCollection<string> Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }

        // View model for single appointment
        private AppointmentViewModel _appointmentViewModel;
        public AppointmentViewModel AppointmentViewModel
        {
            get => _appointmentViewModel;
            set
            {
                _appointmentViewModel = value;
                OnPropertyChanged();
            }
        }

        // View model for the most popular services chart
        private MostPopularServicesViewModel _mostPopularServicesViewModel;
        public MostPopularServicesViewModel MostPopularServicesViewModel
        {
            get => _mostPopularServicesViewModel;
            set
            {
                _mostPopularServicesViewModel = value;
                OnPropertyChanged();
            }
        }

        // View model for active doctors
        private ActiveDoctorsViewModel _activeDoctorsViewModel;
        public ActiveDoctorsViewModel ActiveDoctorsViewModel
        {
            get => _activeDoctorsViewModel;
            set
            {
                _activeDoctorsViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SetSelectedDayCommand { get; }
        public AsyncRelayCommand SetCurrentAppointmentCommand { get; }

        public AdminDashboardViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, INavigationService navigation, IUserSessionService userSessionService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;

            AppointmentViewModel = new AppointmentViewModel(ExitAppointment, RefreshCurrentAppointment, contextFactory);
            MostPopularServicesViewModel = new MostPopularServicesViewModel(contextFactory, userSessionService);
            ActiveDoctorsViewModel = new ActiveDoctorsViewModel(contextFactory);

            SetSelectedDayCommand = new RelayCommand(SetSelectedDay);
            SetCurrentAppointmentCommand = new AsyncRelayCommand(SetCurrentAppointment);

            IsAppointmentDisplayed = false;

            Statuses = new() { "Scheduled", "In Progress", "Completed", "Cancelled" };

            _ = FetchAdminData();
        }

        private Task ExitAppointment()
        {
            IsAppointmentDisplayed = false;
            AppointmentViewModel.DetailedAppointment = null;
            return Task.CompletedTask;
        }

        private void SetSelectedDay(object obj)
        {
            if (obj is CalendarDay day)
            {
                // Select clicked day
                foreach (var calendarDay in CalendarDays)
                {
                    calendarDay.IsSelected = calendarDay.Date.Date == day.Date.Date;
                }

                // Filter appointments for the selected day
                SelectedDayAppointments.Clear();
                foreach (var appointment in AllAppointments.Where(a => a.Appointment.AppointmentDate.Date == day.Date.Date))
                {
                    SelectedDayAppointments.Add(appointment);
                }
            }
        }

        private async Task SetCurrentAppointment(object obj)
        {
            if (obj is DetailedAppointment detailedAppointment)
            {
                IsAppointmentDisplayed = true;
                AppointmentViewModel.DetailedAppointment = detailedAppointment;
            }
        }

        private async Task RefreshCurrentAppointment(int appointmentId)
        {
            using var context = _contextFactory.CreateDbContext();
            var refreshedAppointment = await context.Appointment
                .Include(a => a.Pet)
                    .ThenInclude(p => p.User)
                .Include(a => a.Doctor)
                .Include(a => a.Prescription)
                    .ThenInclude(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                .Include(a => a.AppointmentServices)
                    .ThenInclude(asv => asv.Service)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (refreshedAppointment != null)
            {
                var detailedAppointment = new DetailedAppointment()
                {
                    Appointment = refreshedAppointment,
                    Pet = refreshedAppointment.Pet,
                    Client = refreshedAppointment.Pet.User,
                    Doctor = refreshedAppointment.Doctor,
                    Statuses = new(Statuses),
                    Prescription = refreshedAppointment.Prescription,
                    Services = refreshedAppointment.AppointmentServices.Select(a => a.Service).Where(s => s != null).ToList()
                };

                // Update in both collections
                var allIndex = AllAppointments.IndexOf(AllAppointments.FirstOrDefault(a => a.Appointment.Id == appointmentId));
                if (allIndex >= 0)
                {
                    AllAppointments[allIndex] = detailedAppointment;
                }

                var selectedIndex = SelectedDayAppointments.IndexOf(SelectedDayAppointments.FirstOrDefault(a => a.Appointment.Id == appointmentId));
                if (selectedIndex >= 0)
                {
                    SelectedDayAppointments[selectedIndex] = detailedAppointment;
                }
            }
        }

        private async Task FetchAdminData()
        {
            var admin = _userSessionService.LoggedInUser;
            using var context = _contextFactory.CreateDbContext();

            _admin = await context.User
                .FirstOrDefaultAsync(u => u.Id == admin.Id);

            if (_admin != null)
            {
                FullName = $"{_admin.Name} {_admin.Surname}";

                // Calculate ratios based on the last 7 days and the previous 7 days
                DateTime today = DateTime.Today;
                DateTime last7Start = today.AddDays(-6);
                DateTime prev7Start = today.AddDays(-13);
                DateTime prev7End = today.AddDays(-7);

                await GetAppointmentsCount(prev7Start, prev7End, last7Start, today);
                await GetClientsCount(prev7Start, prev7End, last7Start, today);
                await GetPetsCount(prev7Start, prev7End, last7Start, today);
                await GetRevenueAmount(prev7Start, prev7End, last7Start, today);
                await GetAllAppointments();

                FillCalendar();

                // Initially load today's appointments
                var todayAppointments = AllAppointments.Where(a => a.Appointment.AppointmentDate.Date == today).ToList();
                SelectedDayAppointments.Clear();
                foreach (var appointment in todayAppointments)
                {
                    SelectedDayAppointments.Add(appointment);
                }
            }
            else
            {
                Debug.WriteLine("Admin user not found.");
            }
        }

        private void FillCalendar()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek + 1);

            CalendarDays.Clear();
            for (int i = 0; i < 7; i++)
            {
                DateTime date = firstDayOfWeek.AddDays(i);
                CalendarDays.Add(new CalendarDay
                {
                    Date = date,
                    IsSelected = date.Date == today.Date
                });
            }
        }

        private async Task GetAllAppointments()
        {
            using var context = _contextFactory.CreateDbContext();

            var appointments = await context.Appointment
                .Include(a => a.Pet)
                    .ThenInclude(p => p.User)
                .Include(a => a.Doctor)
                .Include(a => a.Prescription)
                    .ThenInclude(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                .Include(a => a.AppointmentServices)
                    .ThenInclude(asv => asv.Service)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            AllAppointments.Clear();

            foreach (var appointment in appointments)
            {
                AllAppointments.Add(new DetailedAppointment
                {
                    Appointment = appointment,
                    Pet = appointment.Pet,
                    Client = appointment.Pet.User,
                    Doctor = appointment.Doctor,
                    Statuses = new(Statuses),
                    Prescription = appointment.Prescription,
                    Services = appointment.AppointmentServices.Select(a => a.Service).Where(s => s != null).ToList()
                });
            }
        }

        private async Task GetAppointmentsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int last7Count = await context.Appointment
                .CountAsync(a => a.AppointmentDate >= last7Start && a.AppointmentDate <= today);

            int prev7Count = await context.Appointment
                .CountAsync(a => a.AppointmentDate >= prev7Start && a.AppointmentDate <= prev7End);

            AppointmentsCount = last7Count;

            if (prev7Count == 0)
            {
                AppointmentsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                AppointmentsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        private async Task GetClientsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int last7Count = await context.User
                .Where(u => u.Role == "Client")
                .CountAsync(u => u.CreatedAt >= last7Start && u.CreatedAt <= today);

            int prev7Count = await context.User
                .Where(u => u.Role == "Client")
                .CountAsync(u => u.CreatedAt >= prev7Start && u.CreatedAt <= prev7End);

            ClientsCount = last7Count;

            if (prev7Count == 0)
            {
                ClientsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                ClientsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        private async Task GetPetsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int last7Count = await context.Pet
                .CountAsync(p => p.CreatedAt >= last7Start && p.CreatedAt <= today);

            int prev7Count = await context.Pet
                .CountAsync(p => p.CreatedAt >= prev7Start && p.CreatedAt <= prev7End);

            PetsCount = last7Count;

            if (prev7Count == 0)
            {
                PetsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                PetsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        private async Task GetRevenueAmount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            decimal last7Revenue = await context.Appointment
                .Where(a => a.AppointmentDate >= last7Start && a.AppointmentDate <= today && a.Status == "Completed")
                .Include(a => a.AppointmentServices)
                    .ThenInclude(asv => asv.Service)
                .SelectMany(a => a.AppointmentServices)
                .Where(asv => asv.Service != null)
                .SumAsync(asv => asv.Service.Price);

            decimal prev7Revenue = await context.Appointment
                .Where(a => a.AppointmentDate >= prev7Start && a.AppointmentDate <= prev7End && a.Status == "Completed")
                .Include(a => a.AppointmentServices)
                    .ThenInclude(asv => asv.Service)
                .SelectMany(a => a.AppointmentServices)
                .Where(asv => asv.Service != null)
                .SumAsync(asv => asv.Service.Price);

            RevenueAmount = last7Revenue;

            if (prev7Revenue == 0)
            {
                RevenueRatio = last7Revenue > 0 ? 100 : 0;
            }
            else
            {
                RevenueRatio = ((double)(last7Revenue - prev7Revenue) / (double)prev7Revenue) * 100;
            }
        }
    }
}