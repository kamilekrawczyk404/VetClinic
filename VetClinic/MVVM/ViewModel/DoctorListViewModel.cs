﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class DoctorListViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        public DoctorListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            Doctors = new ObservableCollection<Doctor>();

            AddDoctorCommand = new RelayCommand(AddDoctor, _ => IsAdmin);
            EditDoctorCommand = new RelayCommand(EditDoctor, _ => IsAdmin);
            DeleteDoctorCommand = new RelayCommand(async doctor => await DeleteDoctor(doctor), _ => IsAdmin);
            ViewOpinionsCommand = new RelayCommand(ViewOpinions);
            BookAppointmentCommand = new RelayCommand(BookAppointment, _ => IsClient);

            _userSessionService.UserChanged += async () => await OnUserChanged();
            _ = LoadDoctorsAsync();
        }

        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool IsClient => _userSessionService.IsClient;

        public RelayCommand AddDoctorCommand { get; }
        public RelayCommand EditDoctorCommand { get; }
        public RelayCommand DeleteDoctorCommand { get; }
        public RelayCommand ViewOpinionsCommand { get; }
        public RelayCommand BookAppointmentCommand { get; }

        private async Task OnUserChanged()
        {
            await LoadDoctorsAsync();
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsClient));
        }

        private async Task LoadDoctorsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                Trace.WriteLine("Loading doctors from database...");

                var doctors = await context.Doctor
                    .OrderBy(d => d.Surname)
                    .ThenBy(d => d.Name)
                    .ToListAsync();

                Doctors = new ObservableCollection<Doctor>(doctors);

                Trace.WriteLine($"Loaded {doctors.Count} doctors from database");

                // Dodatkowe logowanie dla debugowania
                foreach (var doctor in doctors)
                {
                    Trace.WriteLine($"Doctor: {doctor.Name} {doctor.Surname} (ID: {doctor.Id})");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch doctors: {ex.Message}");
                Trace.TraceError($"Stack trace: {ex.StackTrace}");
                Doctors = new ObservableCollection<Doctor>();
            }
        }

        private async void AddDoctor(object obj)
        {
            if (!IsAdmin) return;

            _navigationService.NavigateTo<DoctorEditViewModel>();

            // Odświeżenie listy po powrocie z edycji
            await Task.Delay(100); // Krótkie opóźnienie aby nawigacja się zakończyła
            await RefreshDoctorsAsync();
        }

        private async void EditDoctor(object obj)
        {
            if (!(obj is Doctor doctor)) return;
            if (!IsAdmin) return;

            _navigationService.NavigateTo<DoctorEditViewModel>(doctor);

            // Odświeżenie listy po powrocie z edycji
            await Task.Delay(100); // Krótkie opóźnienie aby nawigacja się zakończyła
            await RefreshDoctorsAsync();
        }

        private async Task DeleteDoctor(object obj)
        {
            if (!(obj is Doctor doctor)) return;
            if (!IsAdmin) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                var doctorToRemove = await context.Doctor.FindAsync(doctor.Id);
                if (doctorToRemove != null)
                {
                    // Sprawdzenie czy doktor ma nadchodzące lub przeszłe wizyty
                    var hasAppointments = await context.Appointment.AnyAsync(a => a.DoctorId == doctorToRemove.Id);
                    if (hasAppointments)
                    {
                        MessageBox.Show("You cannot delete doctor that has upcoming or past appointments.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var result = MessageBox.Show(
                        $"Are you sure you want to delete doctor {doctor.Name} {doctor.Surname}?",
                        "Delete doctor",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                        return;

                    context.Doctor.Remove(doctorToRemove);
                    await context.SaveChangesAsync();

                    Doctors.Remove(doctor);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error deleting doctor: {ex.Message}");
                MessageBox.Show("An error occurred while deleting the doctor.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewOpinions(object obj)
        {
            if (!(obj is Doctor doctor))
            {
                return;
            }
            _navigationService.NavigateTo<ViewOpinionsViewModel>(doctor);
        }

        private void BookAppointment(object obj)
        {
            if (!(obj is Doctor doctor))
            {
                return;
            }
            _navigationService.NavigateTo<BookAppointmentViewModel>(doctor);
        }

        public async Task RefreshDoctorsAsync()
        {
            await LoadDoctorsAsync();
        }
    }
}