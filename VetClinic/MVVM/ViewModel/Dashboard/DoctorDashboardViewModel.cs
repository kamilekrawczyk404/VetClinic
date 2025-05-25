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

namespace VetClinic.MVVM.ViewModel.Dashboard
{
    public class DoctorDashboardViewModel : ViewModel
    {
        private readonly VeterinaryClinicContext _context;
        private readonly IUserSessionService _userSessionService;

        private Doctor _doctor;

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

        private ObservableCollection<DetailedAppointment> _nextAppointments;
        public ObservableCollection<DetailedAppointment> NextAppointments
        {
            get => _nextAppointments;
            set
            {
                _nextAppointments = value;
                OnPropertyChanged();
            }
        }
        public DoctorDashboardViewModel(VeterinaryClinicContext context, IUserSessionService userSessionService)
        {
            _context = context;
            _userSessionService = userSessionService;

            NextAppointments = new ObservableCollection<DetailedAppointment>();

            _userSessionService.UserChanged += async () => await OnUserChanged();
        }

        private async Task OnUserChanged()
        {
            var user = _userSessionService.LoggedInUser;

            _doctor = await _context.Doctor
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.User == user);

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
                await GetNextAppointments();
            }
            else
            {
                Debug.WriteLine("Doctor not found for the logged-in user.");
            }
        }

        private async Task GetNextAppointments()
        {
            DateTime today = DateTime.Today;
            DateTime nextWeek = today.AddDays(7);

            // feed database with new values...
            var nextAppointments = await _context.Appointment
                .Where(a => a.DoctorId == _doctor.Id && a.AppointmentDate <= nextWeek && a.AppointmentDate >= today)
                .Include(a => a.Pet)
                .ThenInclude(p => p.Client)
                .ThenInclude(c => c.User)
                .Include(c => c.Doctor)
                //.Include(a => a.Prescriptions)
                .OrderBy(a => a.AppointmentDate)
                .ToListAsync();


            foreach (var nextAppointment in nextAppointments)
            {
                NextAppointments.Add(new DetailedAppointment
                {
                    Appointment = nextAppointment,
                    Pet = nextAppointment.Pet,
                    Client = nextAppointment.Pet.Client,
                    Doctor = nextAppointment.Doctor,
                });
            }

            Trace.WriteLine("Length: " + NextAppointments.Count());
        }

        private async Task GetOpinionsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            int last7Count = await _context.Opinion
                .CountAsync(o => o.DoctorId == _doctor.Id && o.CreatedAt >= last7Start && o.CreatedAt <= today);

            int prev7Count = await _context.Opinion
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
            int last7Count = await _context.Prescription
                .CountAsync(p => p.Appointment.DoctorId == _doctor.Id && p.CreatedAt >= last7Start && p.CreatedAt <= today);

            int prev7Count = await _context.Prescription
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
            int last7Count = await _context.Appointment
                .CountAsync(a => a.DoctorId == _doctor.Id && a.AppointmentDate >= last7Start && a.AppointmentDate <= today);

            int prev7Count = await _context.Appointment
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
