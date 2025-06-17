using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Utils;
using VetClinic.MVVM.ViewModel.Dashboard;

namespace VetClinic.MVVM.ViewModel
{
    public class BookAppointmentViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        private DateTime _currentWeekStart;
        private Doctor _preselectedDoctor; 

        public BookAppointmentViewModel(
            IDbContextFactory<VeterinaryClinicContext> contextFactory,
            IUserSessionService userSessionService,
            INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            Doctors = new ObservableCollection<Doctor>();
            UserPets = new ObservableCollection<Pet>();
            WeekDays = new ObservableCollection<CalendarDay>();
            AvailableTimeSlots = new ObservableCollection<TimeSlot>();

            _currentWeekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            PreviousWeekCommand = new RelayCommand(PreviousWeek);
            NextWeekCommand = new RelayCommand(NextWeek);
            SelectDayCommand = new RelayCommand(SelectDay);
            SelectTimeCommand = new RelayCommand(SelectTime);
            BookAppointmentCommand = new RelayCommand(BookAppointment, CanBookAppointment);
            CancelCommand = new RelayCommand(Cancel);

            // Load initial data
            _ = LoadInitialDataAsync();
        }

        #region Properties

        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set { _doctors = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Pet> _userPets;
        public ObservableCollection<Pet> UserPets
        {
            get => _userPets;
            set { _userPets = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CalendarDay> _weekDays;
        public ObservableCollection<CalendarDay> WeekDays
        {
            get => _weekDays;
            set { _weekDays = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TimeSlot> _availableTimeSlots;
        public ObservableCollection<TimeSlot> AvailableTimeSlots
        {
            get => _availableTimeSlots;
            set { _availableTimeSlots = value; OnPropertyChanged(); }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasValidSelection));
                OnPropertyChanged(nameof(AppointmentSummary));
                OnPropertyChanged(nameof(SelectedDoctorDisplay));
                _ = LoadAvailableTimeSlotsAsync();
            }
        }

        private Pet _selectedPet;
        public Pet SelectedPet
        {
            get => _selectedPet;
            set
            {
                _selectedPet = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasValidSelection));
                OnPropertyChanged(nameof(AppointmentSummary));
                OnPropertyChanged(nameof(SelectedPetDisplay));
            }
        }

        private string _reasonForVisit;
        public string ReasonForVisit
        {
            get => _reasonForVisit;
            set
            {
                _reasonForVisit = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasValidSelection));
                OnPropertyChanged(nameof(AppointmentSummary));
            }
        }

        private string _notes;

        private CalendarDay _selectedDay;
        public CalendarDay SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (_selectedDay != null)
                    _selectedDay.IsSelected = false;

                _selectedDay = value;

                if (_selectedDay != null)
                    _selectedDay.IsSelected = true;

                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowTimeSlots));
                OnPropertyChanged(nameof(SelectedDateDisplay));
                OnPropertyChanged(nameof(HasValidSelection));
                OnPropertyChanged(nameof(AppointmentSummary));

                _ = LoadAvailableTimeSlotsAsync();
            }
        }

        private TimeSlot _selectedTimeSlot;
        public TimeSlot SelectedTimeSlot
        {
            get => _selectedTimeSlot;
            set
            {
                if (_selectedTimeSlot != null)
                    _selectedTimeSlot.IsSelected = false;

                _selectedTimeSlot = value;

                if (_selectedTimeSlot != null)
                    _selectedTimeSlot.IsSelected = true;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasValidSelection));
                OnPropertyChanged(nameof(AppointmentSummary));
            }
        }

        private bool _isLoadingTimeSlots;
        public bool IsLoadingTimeSlots
        {
            get => _isLoadingTimeSlots;
            set
            {
                _isLoadingTimeSlots = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NoTimeSlotsAvailable));
            }
        }

        private string _bookingResult;
        public string BookingResult
        {
            get => _bookingResult;
            set
            {
                _bookingResult = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasBookingResult));
            }
        }

        private bool _isBookingSuccess;
        public bool IsBookingSuccess
        {
            get => _isBookingSuccess;
            set
            {
                _isBookingSuccess = value;
                OnPropertyChanged();
            }
        }

        public string CurrentWeekDisplay =>
            $"{_currentWeekStart:MMM dd} - {_currentWeekStart.AddDays(6):MMM dd, yyyy}";

        public bool ShowTimeSlots => SelectedDay != null;

        public string SelectedDateDisplay => SelectedDay?.Date.ToString("dddd, MMMM dd, yyyy") ?? "";

        public bool NoTimeSlotsAvailable =>
            !IsLoadingTimeSlots && SelectedDay != null && AvailableTimeSlots.Count == 0;

        public bool HasValidSelection =>
            SelectedDoctor != null &&
            SelectedPet != null &&
            !string.IsNullOrWhiteSpace(ReasonForVisit) &&
            SelectedDay != null &&
            SelectedTimeSlot != null;

        public bool HasBookingResult => !string.IsNullOrEmpty(BookingResult);

        public string SelectedDoctorDisplay => SelectedDoctor != null
            ? $"Dr. {SelectedDoctor.Name} {SelectedDoctor.Surname} - {SelectedDoctor.Specialization}"
            : "";

        public string SelectedPetDisplay => SelectedPet != null
            ? $"{SelectedPet.Name} ({SelectedPet.Species})"
            : "";

        public string AppointmentSummary
        {
            get
            {
                if (!HasValidSelection) return "";

                return $"{SelectedPet.Name} with {SelectedDoctor.Surname} on {SelectedDay.Date:MMM dd} at {SelectedTimeSlot.Time:HH:mm}";
            }
        }

        #endregion

        #region Commands

        public ICommand PreviousWeekCommand { get; }
        public ICommand NextWeekCommand { get; }
        public ICommand SelectDayCommand { get; }
        public ICommand SelectTimeCommand { get; }
        public ICommand BookAppointmentCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Command Methods

        private void PreviousWeek(object parameter)
        {
            _currentWeekStart = _currentWeekStart.AddDays(-7);
            GenerateWeekDays();
            OnPropertyChanged(nameof(CurrentWeekDisplay));
        }

        private void NextWeek(object parameter)
        {
            _currentWeekStart = _currentWeekStart.AddDays(7);
            GenerateWeekDays();
            OnPropertyChanged(nameof(CurrentWeekDisplay));
        }

        private void SelectDay(object parameter)
        {
            if (parameter is CalendarDay day)
            {
                SelectedDay = day;
            }
        }

        private void SelectTime(object parameter)
        {
            if (parameter is TimeSlot timeSlot)
            {
                SelectedTimeSlot = timeSlot;
            }
        }

        private async void BookAppointment(object parameter)
        {
            if (!HasValidSelection) return;

            try
            {
                using var context = _contextFactory.CreateDbContext();

                var appointment = new Appointment
                {
                    PetId = SelectedPet.Id,
                    DoctorId = SelectedDoctor.Id,
                    AppointmentDate = SelectedDay.Date.Add(SelectedTimeSlot.Time.TimeOfDay),
                    ReasonForVisit = ReasonForVisit,
                    Status = "Scheduled",
                    Diagnosis = "",
                    Notes = "",
                    CreatedAt = DateTime.Now
                };

                context.Appointment.Add(appointment);
                await context.SaveChangesAsync();

                BookingResult = "Appointment booked successfully!";
                IsBookingSuccess = true;

                _navigationService.NavigateTo<AppointmentListViewModel>();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error booking appointment: {ex.Message}");
                BookingResult = "Failed to book appointment. Please try again.";
                IsBookingSuccess = false;
            }
        }

        private bool CanBookAppointment(object parameter)
        {
            return HasValidSelection;
        }

        private void Cancel(object parameter)
        {
            _navigationService.NavigateTo<AppointmentListViewModel>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the preselected doctor. This method should be called from the navigation service.
        /// </summary>
        /// <param name="doctor">The doctor to preselect</param>
        public void SetPreselectedDoctor(Doctor doctor)
        {
            _preselectedDoctor = doctor;

            if (Doctors != null && Doctors.Count > 0)
            {
                SetSelectedDoctorFromPreselected();
            }
        }

        #endregion

        #region Private Methods

        private async Task LoadInitialDataAsync()
        {
            await LoadDoctorsAsync();
            await LoadUserPetsAsync();
            GenerateWeekDays();
        }

        private async Task LoadDoctorsAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var doctors = await context.Doctor
                    .OrderBy(d => d.Surname)
                    .ThenBy(d => d.Name)
                    .ToListAsync();

                Doctors = new ObservableCollection<Doctor>(doctors);

                SetSelectedDoctorFromPreselected();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading doctors: {ex.Message}");
            }
        }

        private void SetSelectedDoctorFromPreselected()
        {
            if (_preselectedDoctor != null && Doctors != null)
            {
                // Find the doctor in the loaded collection by ID
                var doctorToSelect = Doctors.FirstOrDefault(d => d.Id == _preselectedDoctor.Id);
                if (doctorToSelect != null)
                {
                    SelectedDoctor = doctorToSelect;
                }
            }
        }

        private async Task LoadUserPetsAsync()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var pets = await context.Pet
                    .Where(p => p.UserId == _userSessionService.LoggedInUser.Id)
                    .OrderBy(p => p.Name)
                    .ToListAsync();

                UserPets = new ObservableCollection<Pet>(pets);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading user pets: {ex.Message}");
            }
        }

        private void GenerateWeekDays()
        {
            var days = new ObservableCollection<CalendarDay>();

            for (int i = 0; i < 7; i++)
            {
                var date = _currentWeekStart.AddDays(i);

                if (date < DateTime.Today)
                    continue;
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                days.Add(new CalendarDay
                {
                    Date = date,
                    DayName = date.ToString("ddd").ToUpper(),
                    DayNumber = date.Day.ToString(),
                    MonthName = date.ToString("MMM").ToUpper(),
                    IsToday = date.Date == DateTime.Today,
                    IsSelected = false
                });
            }

            WeekDays = days;
        }

        private async Task LoadAvailableTimeSlotsAsync()
        {
            if (SelectedDay == null || SelectedDoctor == null)
            {
                AvailableTimeSlots = new ObservableCollection<TimeSlot>();
                return;
            }

            IsLoadingTimeSlots = true;

            try
            {
                using var context = _contextFactory.CreateDbContext();

                // Get all appointments for the selected doctor and date
                var existingAppointments = await context.Appointment
                    .Where(a => a.DoctorId == SelectedDoctor.Id &&
                               a.AppointmentDate.Date == SelectedDay.Date.Date &&
                               a.Status != "Cancelled")
                    .Select(a => a.AppointmentDate.TimeOfDay)
                    .ToListAsync();

                var timeSlots = new ObservableCollection<TimeSlot>();
                var startTime = new TimeSpan(8, 0, 0);
                var endTime = new TimeSpan(16, 0, 0);
                var slotDuration = new TimeSpan(0, 30, 0);

                for (var time = startTime; time < endTime; time = time.Add(slotDuration))
                {
                    if (existingAppointments.Contains(time))
                        continue;

                    // Skip past times for today
                    if (SelectedDay.Date.Date == DateTime.Today &&
                        DateTime.Now.TimeOfDay > time.Add(TimeSpan.FromMinutes(30)))
                        continue;

                    if (SelectedDay.Date.DayOfWeek == DayOfWeek.Saturday || SelectedDay.Date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        AvailableTimeSlots = new ObservableCollection<TimeSlot>();
                        return;
                    }

                    timeSlots.Add(new TimeSlot
                    {
                        Time = DateTime.Today.Add(time),
                        TimeDisplay = DateTime.Today.Add(time).ToString("HH:mm"),
                        IsSelected = false
                    });
                }

                AvailableTimeSlots = timeSlots;
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error loading time slots: {ex.Message}");
                AvailableTimeSlots = new ObservableCollection<TimeSlot>();
            }
            finally
            {
                IsLoadingTimeSlots = false;
            }
        }

        #endregion
    }

    // Helper classes
    public class CalendarDay : ObservableObject
    {
        private bool _isSelected;

        public DateTime Date { get; set; }
        public string DayName { get; set; }
        public string DayNumber { get; set; }
        public string MonthName { get; set; }
        public bool IsToday { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }

    public class TimeSlot : ObservableObject
    {
        private bool _isSelected;

        public DateTime Time { get; set; }
        public string TimeDisplay { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }
}