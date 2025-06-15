using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel;
using VetClinic.Services;
using VetClinic.Utils;


namespace VetClinic.MVVM.ViewModel
{
    class AppointmentListViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        public AppointmentListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            UpcomingAppointments = new ObservableCollection<Appointment>();
            PastAppointments = new ObservableCollection<Appointment>();

            CancelAppointmentCommand = new RelayCommand(CancelAppointment, CanCancelAppointment);

            _userSessionService.UserChanged += async () => await OnUserChanged();
            _ = LoadAppointmentsAsync();
        }

        private ObservableCollection<Appointment> _upcomingAppointments;
        public ObservableCollection<Appointment> UpcomingAppointments
        {
            get => _upcomingAppointments;
            set
            {
                _upcomingAppointments = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Appointment> _pastAppointments;
        public ObservableCollection<Appointment> PastAppointments
        {
            get => _pastAppointments;
            set
            {
                _pastAppointments = value;
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
        public int? CurrentUserId => _userSessionService.LoggedInUser?.Id;

        public RelayCommand CancelAppointmentCommand { get; }
        public RelayCommand RefreshCommand { get; }

        private async Task OnUserChanged()
        {
            await LoadAppointmentsAsync();
        }

        private async Task LoadAppointmentsAsync()
        {
            if (!IsClient || CurrentUserId == null)
            {
                UpcomingAppointments = new ObservableCollection<Appointment>();
                PastAppointments = new ObservableCollection<Appointment>();
                return;
            }

            IsLoading = true;
            using var context = _contextFactory.CreateDbContext();

            try
            {
                var now = DateTime.Now;

                // Pobierz wszystkie wizyty klienta z relacjami (przez Pet.UserId)
                var allAppointments = await context.Appointment
                    .Include(a => a.Doctor)
                    .Include(a => a.Pet)
                    .Where(a => a.Pet.UserId == CurrentUserId)
                    .OrderBy(a => a.AppointmentDate)
                    .ToListAsync();

                // Podziel na nadchodzące i przeszłe
                var upcoming = allAppointments
                    .Where(a => a.AppointmentDate >= now && a.Status != "Cancelled")
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();

                var past = allAppointments
                    .Where(a => a.AppointmentDate < now || a.Status == "Cancelled" || a.Status == "Completed")
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToList();

                UpcomingAppointments = new ObservableCollection<Appointment>(upcoming);
                PastAppointments = new ObservableCollection<Appointment>(past);

                Trace.WriteLine($"Loaded {upcoming.Count} upcoming and {past.Count} past appointments for client {CurrentUserId}");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch appointments: {ex.Message}");
                Trace.TraceError($"Stack trace: {ex.StackTrace}");
                UpcomingAppointments = new ObservableCollection<Appointment>();
                PastAppointments = new ObservableCollection<Appointment>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool CanCancelAppointment(object obj)
        {
            if (!(obj is Appointment appointment))
                return false;

            // Można anulować tylko nadchodzące wizyty (nie anulowane, nie zakończone)
            return appointment.Status != "Cancelled" &&
                   appointment.Status != "Completed" &&
                   appointment.AppointmentDate > DateTime.Now;
        }

        private async void CancelAppointment(object obj)
        {
            if (!(obj is Appointment appointment))
                return;

            // Potwierdzenie anulowania
            var result = MessageBox.Show(
                $"Czy na pewno chcesz anulować wizytę z dnia {appointment.AppointmentDate:dd.MM.yyyy HH:mm}?",
                "Potwierdzenie anulowania",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                using var context = _contextFactory.CreateDbContext();

                var appointmentToUpdate = await context.Appointment
                    .FirstOrDefaultAsync(a => a.Id == appointment.Id);

                if (appointmentToUpdate != null)
                {
                    appointmentToUpdate.Status = "Cancelled";
                    await context.SaveChangesAsync();

                    // Odśwież listy
                    await LoadAppointmentsAsync();

                    MessageBox.Show("Wizyta została anulowana.", "Anulowanie wizyty", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error cancelling appointment: {ex.Message}");
                MessageBox.Show("Wystąpił błąd podczas anulowania wizyty.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}

