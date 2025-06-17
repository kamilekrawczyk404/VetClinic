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
    public class ClientDashboardViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;

        private User _client; 

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

        private int _upcomingAppointmentsCount;
        public int UpcomingAppointmentsCount
        {
            get => _upcomingAppointmentsCount;
            set
            {
                _upcomingAppointmentsCount = value;
                OnPropertyChanged();
            }
        }

        private double _upcomingAppointmentsRatio;
        public double UpcomingAppointmentsRatio
        {
            get => _upcomingAppointmentsRatio;
            set
            {
                _upcomingAppointmentsRatio = value;
                OnPropertyChanged();
            }
        }

        private int _activePrescriptionsCount;
        public int ActivePrescriptionsCount
        {
            get => _activePrescriptionsCount;
            set
            {
                _activePrescriptionsCount = value;
                OnPropertyChanged();
            }
        }

        private double _activePrescriptionsRatio;
        public double ActivePrescriptionsRatio
        {
            get => _activePrescriptionsRatio;
            set
            {
                _activePrescriptionsRatio = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailedOpinion> _lastOpinions = new ObservableCollection<DetailedOpinion>();
        public ObservableCollection<DetailedOpinion> LastOpinions
        {
            get => _lastOpinions;
            set
            {
                _lastOpinions = value;
                OnPropertyChanged();
            }
        }

        private bool _hasOpinions;
        public bool HasOpinions
        {
            get => _hasOpinions;
            set
            {
                _hasOpinions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailedPet> _clientPets = new ObservableCollection<DetailedPet>();
        public ObservableCollection<DetailedPet> ClientPets
        {
            get => _clientPets;
            set
            {
                _clientPets = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DetailedAppointment> _upcomingAppointments = new ObservableCollection<DetailedAppointment>();
        public ObservableCollection<DetailedAppointment> UpcomingAppointments
        {
            get => _upcomingAppointments;
            set
            {
                _upcomingAppointments = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand SelectPetCommand { get; }
        public RelayCommand SelectAppointmentCommand { get; }
        public RelayCommand ViewOpinionCommand { get; }
        public AsyncRelayCommand CancelAppointmentCommand { get; }


        public ClientDashboardViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, INavigationService navigation, IUserSessionService userSessionService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;

            SelectPetCommand = new RelayCommand(SelectPet);
            SelectAppointmentCommand = new RelayCommand(SelectAppointment);
            ViewOpinionCommand = new RelayCommand(ViewOpinion);
            CancelAppointmentCommand = new AsyncRelayCommand(CancelAppointment);

            _userSessionService.UserChanged += async () => await OnUserChanged();

            _ = LoadDataAsync();

        }
        private async Task LoadDataAsync()
        {
            try
            {
                await OnUserChanged();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading dashboard data: {ex.Message}");
            }
        }

        private void SelectPet(object obj)
        {
            if (obj is DetailedPet pet)
            {
                Debug.WriteLine($"Selected pet: {pet.Pet.Name}");
            }
        }

        private void SelectAppointment(object obj)
        {
            if (obj is DetailedAppointment appointment)
            {
                Debug.WriteLine($"Selected appointment: {appointment.Appointment.AppointmentDate}");
            }
        }

        private void ViewOpinion(object obj)
        {
            if (obj is DetailedOpinion opinion)
            {
                Debug.WriteLine($"Viewing opinion for doctor: {opinion.DoctorName}");
            }
        }

        private async Task OnUserChanged()
        {
            var user = _userSessionService.LoggedInUser;

            if (user != null)
            {
                _client = user; 

                FullName = $"{_client.Name} {_client.Surname}";

                DateTime today = DateTime.Today;
                DateTime last7Start = today.AddDays(-6);
                DateTime prev7Start = today.AddDays(-13);
                DateTime prev7End = today.AddDays(-7);

                await GetPetsCount(prev7Start, prev7End, last7Start, today);
                await GetUpcomingAppointmentsCount(prev7Start, prev7End, last7Start, today);
                await GetActivePrescriptionsCount(prev7Start, prev7End, last7Start, today);
                await GetLastOpinions();
                await GetClientPets();
                await GetUpcomingAppointments();
            }
            else
            {
                Debug.WriteLine("No user logged in.");
            }
        }

        private async Task GetPetsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int currentCount = await context.Pet
                .CountAsync(p => p.UserId == _client.Id);

            int last7Count = await context.Pet
                .CountAsync(p => p.UserId == _client.Id && p.CreatedAt >= last7Start && p.CreatedAt <= today);

            int prev7Count = await context.Pet
                .CountAsync(p => p.UserId == _client.Id && p.CreatedAt >= prev7Start && p.CreatedAt <= prev7End);

            PetsCount = currentCount;

            if (prev7Count == 0)
            {
                PetsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                PetsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        private async Task GetUpcomingAppointmentsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            DateTime nextWeek = today.AddDays(7);

            int currentCount = await context.Appointment
                .CountAsync(a => a.Pet.UserId == _client.Id && a.AppointmentDate >= today && a.AppointmentDate <= nextWeek);

            int last7Count = await context.Appointment
                .CountAsync(a => a.Pet.UserId == _client.Id && a.AppointmentDate >= last7Start && a.AppointmentDate <= today);

            int prev7Count = await context.Appointment
                .CountAsync(a => a.Pet.UserId == _client.Id && a.AppointmentDate >= prev7Start && a.AppointmentDate <= prev7End);

            UpcomingAppointmentsCount = currentCount;

            if (prev7Count == 0)
            {
                UpcomingAppointmentsRatio = last7Count > 0 ? 100 : 0;
            }
            else
            {
                UpcomingAppointmentsRatio = ((double)(last7Count - prev7Count) / prev7Count) * 100;
            }
        }

        private async Task GetActivePrescriptionsCount(DateTime prev7Start, DateTime prev7End, DateTime last7Start, DateTime today)
        {
            using var context = _contextFactory.CreateDbContext();

            int currentCount = await context.Prescription
                .CountAsync(p => p.Appointment.Pet.UserId == _client.Id && p.ExpiryDate >= today);

            ActivePrescriptionsCount = currentCount;

            ActivePrescriptionsRatio = double.NaN;
        }

        private async Task GetLastOpinions()
        {
            using var context = _contextFactory.CreateDbContext();

            var lastOpinions = await context.Opinion
                .Where(o => o.ClientId == _client.Id)
                .Include(o => o.Doctor)
                .OrderByDescending(o => o.CreatedAt)
                .Take(2)
                .ToListAsync();

            LastOpinions.Clear();

            if (lastOpinions.Any())
            {
                foreach (var opinion in lastOpinions)
                {
                    LastOpinions.Add(new DetailedOpinion
                    {
                        Opinion = opinion,
                        DoctorName = $"{opinion.Doctor.Name} {opinion.Doctor.Surname}"
                    });
                }
                HasOpinions = true;
            }
            else
            {
                HasOpinions = false;
            }
        }

        private async Task GetClientPets()
        {
            using var context = _contextFactory.CreateDbContext();
            var pets = await context.Pet
                .Where(p => p.UserId == _client.Id)
                .OrderBy(p => p.Name)
                .ToListAsync();

            ClientPets.Clear();

            foreach (var pet in pets)
            {
                ClientPets.Add(new DetailedPet
                {
                    Pet = pet,
                    Client = _client 
                });
            }
        }

        private async Task GetUpcomingAppointments()
        {
            using var context = _contextFactory.CreateDbContext();

            DateTime today = DateTime.Today;
            DateTime nextTwoWeeks = today.AddDays(14);

            var appointments = await context.Appointment
                .Where(a => a.Pet.UserId == _client.Id && a.AppointmentDate >= today && a.AppointmentDate <= nextTwoWeeks)
                .Include(a => a.Pet)
                .Include(a => a.Doctor)
                .OrderBy(a => a.AppointmentDate)
                .Take(5)
                .ToListAsync();

            UpcomingAppointments.Clear();

            foreach (var appointment in appointments)
            {
                UpcomingAppointments.Add(new DetailedAppointment
                {
                    Appointment = appointment,
                    Pet = appointment.Pet,
                    Client = _client, 
                    Doctor = appointment.Doctor
                });
            }
        }
        private async Task CancelAppointment(object arg)
        {
            if (arg is Appointment appointment)
            {
                if (appointment.Status.ToLower() == "cancelled")
                    return;

                using var context = _contextFactory.CreateDbContext();

                appointment.Status = "Cancelled";
                context.Appointment.Update(appointment);

                await context.SaveChangesAsync();
                await this.GetUpcomingAppointments();
            }
        }
    }


    public class DetailedPet
    {
        public Pet Pet { get; set; }
        public User Client { get; set; } 

        public string DisplayName => Pet?.Name ?? "Unknown";
        public string SpeciesName => Pet?.Species ?? "Unknown";
        public string BreedName => Pet?.Breed ?? "Unknown";
        public string GenderName => Pet?.Gender ?? "Unknown";
        public int Age
        {
            get
            {
                if (Pet?.DateOfBirth != null)
                {
                    var today = DateTime.Today;
                    var age = today.Year - Pet.DateOfBirth.Year;
                    if (Pet.DateOfBirth.Date > today.AddYears(-age)) age--;
                    return age;
                }
                return 0;
            }
        }
        public string AgeText => Age == 1 ? "1 rok" : Age < 5 ? $"{Age} lata" : $"{Age} lat";
        public string WeightText => Pet?.Weight > 0 ? $"{Pet.Weight:F1} kg" : "Brak danych";
    }

    // Helper class for detailed opinion information
    public class DetailedOpinion
    {
        public Opinion Opinion { get; set; }
        public string DoctorName { get; set; }

        public string CommentPreview
        {
            get
            {
                if (string.IsNullOrEmpty(Opinion?.Comment))
                    return "Brak komentarza";

                return Opinion.Comment.Length > 100
                    ? Opinion.Comment.Substring(0, 100) + "..."
                    : Opinion.Comment;
            }
        }

        public string RatingText => Opinion?.Rating.ToString() ?? "0";
        public string DateText => Opinion?.CreatedAt.ToString("dd.MM.yyyy") ?? "";
        public string TimeAgoText
        {
            get
            {
                if (Opinion?.CreatedAt == null) return "";

                var timeSpan = DateTime.Now - Opinion.CreatedAt;
                if (timeSpan.Days > 0)
                    return $"{timeSpan.Days} dni temu";
                else if (timeSpan.Hours > 0)
                    return $"{timeSpan.Hours} godzin temu";
                else
                    return $"{timeSpan.Minutes} minut temu";
            }
        }
    }
}