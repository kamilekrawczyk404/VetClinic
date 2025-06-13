using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.Model;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class ActiveDoctorsViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;

        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged();
                UpdateEmptyState();
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
                UpdateEmptyState();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private Doctor _selectedDoctor;
        public Doctor SelectedDoctor
        {
            get => _selectedDoctor;
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand RefreshCommand { get; }
        public AsyncRelayCommand LoadDoctorsCommand { get; }

        public ActiveDoctorsViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _doctors = new ObservableCollection<Doctor>();

            RefreshCommand = new RelayCommand(async (obj) => await RefreshAsync());
            LoadDoctorsCommand = new AsyncRelayCommand(LoadDoctorsAsync);

            // Load doctors on initialization
            _ = LoadDoctorsAsync();
        }

        private async Task LoadDoctorsAsync(object parameter = null)
        {
            IsLoading = true;

            try
            {
                using var context = _contextFactory.CreateDbContext();

                var doctors = await context.Doctor
                    .OrderBy(d => d.Name)
                    .ThenBy(d => d.Surname)
                    .ToListAsync();

                Doctors.Clear();
                foreach (var doctor in doctors)
                {
                    Doctors.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                // Handle exception - you might want to log this or show error message
                // For now, just ensure the collection is cleared
                Doctors.Clear();

                // You could add error handling here similar to other ViewModels in your project
                System.Diagnostics.Debug.WriteLine($"Error loading doctors: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task RefreshAsync()
        {
            await LoadDoctorsAsync();
        }

        private void UpdateEmptyState()
        {
            IsEmpty = !IsLoading && (Doctors == null || Doctors.Count == 0);
        }

        // Method to get doctor details if needed
        public async Task<Doctor> GetDoctorDetailsAsync(int doctorId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Doctor
                .FirstOrDefaultAsync(d => d.Id == doctorId);
        }

        // Method to get doctor statistics if needed in the future
        public async Task<int> GetDoctorAppointmentCountAsync(int doctorId, DateTime? fromDate = null)
        {
            using var context = _contextFactory.CreateDbContext();
            var query = context.Appointment.Where(a => a.DoctorId == doctorId);

            if (fromDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate >= fromDate.Value);
            }

            return await query.CountAsync();
        }

        // Method to search doctors by specialization
        public async Task<List<Doctor>> GetDoctorsBySpecializationAsync(string specialization)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Doctor
                .Where(d => d.Specialization.Contains(specialization))
                .OrderBy(d => d.Name)
                .ThenBy(d => d.Surname)
                .ToListAsync();
        }
    }
}