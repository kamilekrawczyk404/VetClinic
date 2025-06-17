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
            BookAppointmentCommand = new RelayCommand(BookAppointment);


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
        public RelayCommand BookAppointmentCommand { get; }

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

                var allAppointments = await context.Appointment
                    .Include(a => a.Doctor)
                    .Include(a => a.Pet)
                    .Where(a => a.Pet.UserId == CurrentUserId)
                    .OrderBy(a => a.AppointmentDate)
                    .ToListAsync();

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

            return appointment.Status != "Cancelled" &&
                   appointment.Status != "Completed" &&
                   appointment.AppointmentDate > DateTime.Now;
        }

        private async void CancelAppointment(object obj)
        {
            if (!(obj is Appointment appointment))
                return;

            var result = MessageBox.Show(
                $"Are you sure you want to cancel the appointment scheduled for {appointment.AppointmentDate:dd.MM.yyyy HH:mm}?",
                "Cancel Appointment Confirmation",
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

                    var upcomingToRemove = UpcomingAppointments.FirstOrDefault(a => a.Id == appointment.Id);
                    if (upcomingToRemove != null)
                    {
                        UpcomingAppointments.Remove(upcomingToRemove);

                        upcomingToRemove.Status = "Cancelled";

                        PastAppointments.Insert(0, upcomingToRemove);
                    }

                    MessageBox.Show("The appointment has been cancelled.", "Appointment Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error cancelling appointment: {ex.Message}");
                MessageBox.Show("An error occurred while cancelling the appointment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                await LoadAppointmentsAsync();
            }
        }

        private void BookAppointment(object parameter)
        {
            _navigationService.NavigateTo<BookAppointmentViewModel>();
        }



    }

}

