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
    public class DoctorDashboardViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;

        private Doctor _doctor;

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

        private double _prescriptionsRatio;
        public double PrescriptionsRatio
        {
            get => _prescriptionsRatio;
            set
            {
                _prescriptionsRatio = value;
                OnPropertyChanged();
            }
        }

        private int _opinionsCount;
        public int OpinionsCount
        {
            get => _opinionsCount;
            set
            {
                _opinionsCount = value;
                OnPropertyChanged();
            }
        }

        private int _prescriptionsCount;
        public int PrescriptionsCount
        {
            get => _prescriptionsCount;
            set
            {
                _prescriptionsCount = value;
                OnPropertyChanged();
            }
        }


        private double _opinionsRatio;
        public double OpinionsRatio
        {
            get => _opinionsRatio;
            set
            {
                _opinionsRatio = value;
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

        public ObservableCollection<DetailedAppointment> _doctorAppointments = new ObservableCollection<DetailedAppointment>();
        public ObservableCollection<DetailedAppointment> DoctorAppointments
        {
            get => _doctorAppointments;
            set
            {
                _doctorAppointments = value;
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

        // View model for the main chart
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

        public DoctorDashboardViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, INavigationService navigation, IUserSessionService userSessionService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;

            AppointmentViewModel = new AppointmentViewModel(ExitAppointment, GetDoctorsAppointments, contextFactory);
            MostPopularServicesViewModel = new MostPopularServicesViewModel(contextFactory, userSessionService);

            SetSelectedDayCommand = new RelayCommand(SetSelectedDay);
            SetCurrentAppointmentCommand = new AsyncRelayCommand(SetCurrentAppointment);

            //_userSessionService.UserChanged += async () => await OnUserChanged();

            Statuses = new ObservableCollection<string>(GetAppointmentStatuses());
            IsAppointmentDisplayed = false;

            FetchDoctorData();
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
                // select clicked appointment
                foreach (var calendarDay in CalendarDays)
                {
                    calendarDay.IsSelected = calendarDay.Date.Date == day.Date.Date;
                }

                // filter only appointments for the selected day
                SelectedDayAppointments.Clear();
                foreach (var appointment in DoctorAppointments.Where(a => a.Appointment.AppointmentDate.Date == day.Date.Date))
                {
                    SelectedDayAppointments.Add(appointment);
                }
            }
        }

        private async Task SetCurrentAppointment(object obj)
        {
            if (obj is DetailedAppointment detailedAppointment)
            {
                using var context = _contextFactory.CreateDbContext();

                // Change status to in progress
                detailedAppointment.Appointment.Status = "In Progress";

                //context.Appointment.Update(appointment.Appointment);
                //await context.SaveChangesAsync();
                //await GetDoctorsAppointments();

                IsAppointmentDisplayed = true;

                AppointmentViewModel.DetailedAppointment = detailedAppointment;
            }
        }

        private async Task FetchDoctorData()
        {
            var doctor = _userSessionService.LoggedInDoctor;
            using var context = _contextFactory.CreateDbContext();            

            _doctor = await context.Doctor
                .FirstOrDefaultAsync(d => d.Id == doctor.Id);

            if (_doctor != null)
            {
                FullName = $"{_doctor.Name} {_doctor.Surname}";

                // Calculate ratios based on the last 7 days and the previous 7 days
                DateTime today = DateTime.Today;
                DateTime last7Start = today.AddDays(-6);
                DateTime prev7Start = today.AddDays(-13);
                DateTime prev7End = today.AddDays(-7);

                await GetOpinionsCount(prev7Start, prev7End, last7Start, today);
                await GetAppointsmentCount(prev7Start, prev7End, last7Start, today);
                await GetPrescriptionsCount(prev7Start, prev7End, last7Start, today);
                await GetDoctorsAppointments();

                FillCalendar();
            }
            else
            {
                Debug.WriteLine("Doctor not found for the logged-in user.");
            }
        }

        private void FillCalendar()
        {
            // Get the first day of the week for the calender
            DateTime today = DateTime.Today;
            DateTime firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek + 1);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

            for(int i = 0; i < 7; i++)
            {
                DateTime date = firstDayOfWeek.AddDays(i);
                CalendarDays.Add(new CalendarDay
                {
                    Date = date,
                    //IsSelected = i == 0,
                    IsSelected = false
                });
            }
        }

        private List<string> GetAppointmentStatuses()
        {
            return new List<string>() { "Sheduled", "In Progress", "Completed", "Cancelled" };
        }

        private async Task GetDoctorsAppointments()
        {
            using var context = _contextFactory.CreateDbContext();

            DateTime today = DateTime.Today;
            DateTime nextWeek = today.AddDays(7);

            // feed database with new values...
            var appointments = await context.Appointment
                //.Where(a => a.DoctorId == _doctor.Id && a.AppointmentDate <= nextWeek && a.AppointmentDate >= today)
                .Where(a => a.DoctorId == _doctor.Id)
                .Include(a => a.Pet)
                    .ThenInclude(p => p.User)
                .Include(c => c.Doctor)
                .Include(a => a.Prescription)
                    .ThenInclude(p => p.PrescriptionDrugs)
                        .ThenInclude(pd => pd.Drug)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();

            DoctorAppointments.Clear();

            foreach (var appointment in appointments)
            {
                DoctorAppointments.Add(new DetailedAppointment
                {
                    Appointment = appointment,
                    Pet = appointment.Pet,
                    Client = appointment.Pet.User,
                    Doctor = appointment.Doctor,
                    Statuses = Statuses,
                    Prescription = appointment.Prescription,
                });
            }

            // for testing only
            // get first date and assign appointments to the state
            //if (DoctorAppointments.Count > 0)
            //{
            //    var firstAppointmentDate = DoctorAppointments.First().Appointment.AppointmentDate.Date;
            //    SelectedDayAppointments.Clear();

            //    // get only appointems for the first date in the list of
            //    foreach (var appointment in DoctorAppointments.Where(a => a.Appointment.AppointmentDate.Date == firstAppointmentDate))
            //    {
            //        SelectedDayAppointments.Add(appointment);
            //    }
            //}
        }

        private async Task GetOpinionsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int last7Count = await context.Opinion
                .CountAsync(o => o.DoctorId == _doctor.Id && o.CreatedAt >= last7Start && o.CreatedAt <= today);

            int prev7Count = await context.Opinion
                .CountAsync(o => o.DoctorId == _doctor.Id && o.CreatedAt >= prev7Start && o.CreatedAt <= prev7End);

            OpinionsCount = last7Count;

            if (prev7Count == 0)
            {
                OpinionsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                OpinionsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        private async Task GetPrescriptionsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int last7Count = await context.Prescription
                .CountAsync(p => p.Appointment.DoctorId == _doctor.Id && p.CreatedAt >= last7Start && p.CreatedAt <= today);

            int prev7Count = await context.Prescription
                .CountAsync(p => p.Appointment.DoctorId == _doctor.Id && p.CreatedAt >= prev7Start && p.CreatedAt <= prev7End);

            PrescriptionsCount = last7Count;

            if (prev7Count == 0)
            {
                PrescriptionsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                PrescriptionsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        public async Task GetAppointsmentCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int last7Count = await context.Appointment
                .CountAsync(a => a.DoctorId == _doctor.Id && a.AppointmentDate >= last7Start && a.AppointmentDate <= today);

            int prev7Count = await context.Appointment
                .CountAsync(a => a.DoctorId == _doctor.Id && a.AppointmentDate >= prev7Start && a.AppointmentDate <= prev7End);

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
    }
}
