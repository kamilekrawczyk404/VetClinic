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

        public ObservableCollection<DetailedAppointment> _allAppointments = new ObservableCollection<DetailedAppointment>();
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

        private ObservableCollection<AppointmentStatus> _statuses;
        public ObservableCollection<AppointmentStatus> Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Doctor> _doctors = new ObservableCollection<Doctor>();
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
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

        // View model for the main chart - clinic-wide statistics
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

        public RelayCommand SetSelectedDayCommand { get; }
        public AsyncRelayCommand SetCurrentAppointmentCommand { get; }

        public AdminDashboardViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, INavigationService navigation, IUserSessionService userSessionService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;

            AppointmentViewModel = new AppointmentViewModel(ExitAppointment, GetAllAppointments, contextFactory);
            MostPopularServicesViewModel = new MostPopularServicesViewModel(contextFactory, userSessionService);
            ActiveDoctorsViewModel = new ActiveDoctorsViewModel(contextFactory);

            SetSelectedDayCommand = new RelayCommand(SetSelectedDay);
            SetCurrentAppointmentCommand = new AsyncRelayCommand(SetCurrentAppointment);

            _userSessionService.UserChanged += async () => await OnUserChanged();

            IsAppointmentDisplayed = false;
        }

        private Task ExitAppointment()
        {
            IsAppointmentDisplayed = false;
            AppointmentViewModel.Appointment = null;

            return Task.CompletedTask;
        }

        private void SetSelectedDay(object obj)
        {
            if (obj is CalendarDay day)
            {
                // select clicked appointment
                foreach (var calendarDay in CalendarDays)
                {
                    calendarDay.IsSelected = calendarDay.Date.Date == day.Date.Date;
                }

                // filter only appointments for the selected day
                SelectedDayAppointments.Clear();
                foreach (var appointment in AllAppointments.Where(a => a.Appointment.AppointmentDate.Date == day.Date.Date))
                {
                    SelectedDayAppointments.Add(appointment);
                }
            }
        }

        private async Task SetCurrentAppointment(object obj)
        {
            if (obj is DetailedAppointment appointment)
            {
                using var context = _contextFactory.CreateDbContext();

                var inProgressStatus = Statuses.FirstOrDefault(s => s.Id == 2);

                // Change appointment status to In Progress
                if (inProgressStatus != null)
                {
                    appointment.Appointment.AppointmentStatus = inProgressStatus;

                    // Update record in the database
                    context.Appointment.Update(appointment.Appointment);
                    // Uncomment when ready to save changes
                    //await context.SaveChangesAsync();
                    //await GetAllAppointments();
                }

                IsAppointmentDisplayed = true;
                AppointmentViewModel.Appointment = appointment;
            }
        }

        private async Task OnUserChanged()
        {
            var user = _userSessionService.LoggedInUser;
            using var context = _contextFactory.CreateDbContext();

            // Initialize appointment's statuses
            var appointmentStatuses = await GetAppointmentStatuses();
            Statuses = new ObservableCollection<AppointmentStatus>(appointmentStatuses);

            _admin = user;

            if (_admin != null)
            {
                // For admin, we might need to check if there's a Doctor record or use User fields
                // Let's check if this user is also a doctor, otherwise use generic admin name
                var adminRecord = await context.Admin
                    .Include(a => a.User)
                    .FirstOrDefaultAsync(a => a.User == user);

                if (adminRecord != null)
                {
                    FullName = $"{adminRecord.Name} {adminRecord.Surname}";
                }

                // Calculate ratios based on the last 7 days and the previous 7 days
                DateTime today = DateTime.Today;
                DateTime last7Start = today.AddDays(-6);
                DateTime prev7Start = today.AddDays(-13);
                DateTime prev7End = today.AddDays(-7);

                await GetAppointmentsCount(prev7Start, prev7End, last7Start, today);
                await GetClientsCount(prev7Start, prev7End, last7Start, today);
                await GetPetsCount(prev7Start, prev7End, last7Start, today);
                await ActiveDoctorsViewModel.RefreshAsync();
                //await GetRevenueAmount(prev7Start, prev7End, last7Start, today);
                await GetAllAppointments();
                await LoadDoctors();

                FillCalendar();
            }
            else
            {
                Debug.WriteLine("Admin user not found.");
            }
        }

        private void FillCalendar()
        {
            CalendarDays.Clear();

            // Get the first day of the week for the calendar
            DateTime today = DateTime.Today;
            DateTime firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek + 1);

            for (int i = 0; i < 7; i++)
            {
                DateTime date = firstDayOfWeek.AddDays(i);
                CalendarDays.Add(new CalendarDay
                {
                    Date = date,
                    IsSelected = i == 0,
                    //IsSelected = date == today
                });
            }
        }

        private async Task<List<AppointmentStatus>> GetAppointmentStatuses()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.AppointmentStatus.ToListAsync();
        }

        private async Task LoadDoctors()
        {
            using var context = _contextFactory.CreateDbContext();

            var doctors = await context.Doctor
                .Include(d => d.User)
                .ToListAsync();

            Doctors.Clear();
            foreach (var doctor in doctors)
            {
                Doctors.Add(doctor);
            }
        }

        private async Task GetAllAppointments()
        {
            using var context = _contextFactory.CreateDbContext();

            DateTime today = DateTime.Today;
            DateTime nextWeek = today.AddDays(7);

            // Get all appointments in the clinic
            var appointments = await context.Appointment
                .Where(a => a.AppointmentDate >= today && a.AppointmentDate <= nextWeek)
                .Include(a => a.AppointmentStatus)
                .Include(a => a.Pet)
                    .ThenInclude(p => p.Client)
                        .ThenInclude(c => c.User)
                .Include(c => c.Doctor)
                    .ThenInclude(d => d.User)
                .Include(a => a.Prescriptions)
                    .ThenInclude(p => p.PrescriptionDrugs)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            AllAppointments.Clear();

            foreach (var appointment in appointments)
            {
                AllAppointments.Add(new DetailedAppointment
                {
                    Appointment = appointment,
                    Pet = appointment.Pet,
                    Client = appointment.Pet.Client,
                    Doctor = appointment.Doctor,
                    Statuses = Statuses,
                    Prescriptions = new(appointment.Prescriptions ?? Enumerable.Empty<Prescription>()),
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

            int last7Count = await context.Client
                .CountAsync();

            int prev7Count = await context.Client
                .CountAsync();

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

        //private async Task GetRevenueAmount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        //{
        //    using var context = _contextFactory.CreateDbContext();

        //    // Get revenue from appointments with services in the last 7 days
        //    decimal last7Amount = await context.Appointment
        //        .Where(a => a.AppointmentDate >= last7Start && a.AppointmentDate <= today)
        //        .Include(a => a.Service)
        //        .Where(a => a.Service != null)
        //        .SumAsync(a => a.Service.Price);

        //    // Get revenue from appointments with services in the previous 7 days
        //    decimal prev7Amount = await context.Appointment
        //        .Where(a => a.AppointmentDate >= prev7Start && a.AppointmentDate <= prev7End)
        //        .Include(a => a.Service)
        //        .Where(a => a.Service != null)
        //        .SumAsync(a => a.Service.Price);

        //    RevenueAmount = last7Amount;

        //    if (prev7Amount == 0)
        //    {
        //        RevenueRatio = last7Amount > 0 ? 100 : 0;
        //    }
        //    else
        //    {
        //        RevenueRatio = ((double)(last7Amount - prev7Amount) / (double)prev7Amount) * 100;
        //    }
        //}
    }
}