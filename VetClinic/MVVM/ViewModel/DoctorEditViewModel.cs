using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class DoctorEditViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;
        private Doctor _originalDoctor;

        public DoctorEditViewModel(
            IDbContextFactory<VeterinaryClinicContext> contextFactory,
            IUserSessionService userSessionService,
            INavigationService navigationService,
            Doctor doctorToEdit = null)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;
            _originalDoctor = doctorToEdit;

            ValidationErrors = new ObservableCollection<string>();

            InitializeCommands();
            InitializeDoctor();
        }

        public void SetDoctorToEdit(Doctor doctorToEdit)
        {
            _originalDoctor = doctorToEdit;
            InitializeDoctor();
        }

        private void InitializeCommands()
        {
            SaveCommand = new RelayCommand(async _ => await SaveDoctorAsync(), _ => CanSave());
            CancelCommand = new RelayCommand(Cancel);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private Doctor _doctor;
        public Doctor Doctor
        {
            get => _doctor;
            set
            {
                _doctor = value;
                OnPropertyChanged();
            }
        }

        private void InitializeDoctor()
        {
            if (_originalDoctor != null)
            {
                // EDYCJA DOKTORA
                IsAddingDoctor = false;
                Title = $"Edit doctor: {_originalDoctor.Name} {_originalDoctor.Surname}";
                SaveButtonText = "💾 Save changes";

                // Ustawiamy zarówno EditingDoctor jak i Doctor
                EditingDoctor = new Doctor
                {
                    Id = _originalDoctor.Id,
                    Name = _originalDoctor.Name,
                    Surname = _originalDoctor.Surname,
                    Email = _originalDoctor.Email,
                    IsActive = _originalDoctor.IsActive,
                    PasswordHash = _originalDoctor.PasswordHash,
                    TelephoneNumber = _originalDoctor.TelephoneNumber,
                    Specialization = _originalDoctor.Specialization,
                    Description = _originalDoctor.Description,
                    DateOfBirth = _originalDoctor.DateOfBirth,
                    CreatedAt = _originalDoctor.CreatedAt,
                    LastLogin = _originalDoctor.LastLogin
                };

                // Ustawiamy Doctor na oryginalny obiekt (dla porównania)
                Doctor = _originalDoctor;

                // Debug log
                Trace.WriteLine($"Initializing doctor for edit: {_originalDoctor.Name} {_originalDoctor.Surname} (ID: {_originalDoctor.Id})");
            }
            else
            {
                // DODAWANIE NOWEGO DOKTORA
                IsAddingDoctor = true;
                Title = "➕Add new doctor";
                SaveButtonText = "➕Add doctor";

                EditingDoctor = new Doctor
                {
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    CreatedAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    IsActive = true,
                    Specialization = "General Practice"
                };

                // Dla nowego doktora Doctor pozostaje null
                Doctor = null;

                Trace.WriteLine("Initializing new doctor");
            }
        }

        // Properties
        private Doctor _editingDoctor;

        public Doctor EditingDoctor
        {
            get => _editingDoctor;
            set
            {
                _editingDoctor = value;
                OnPropertyChanged();
                ValidateAndUpdateErrors();
            }
        }

        private ObservableCollection<string> _validationErrors;
        public ObservableCollection<string> ValidationErrors
        {
            get => _validationErrors;
            set
            {
                _validationErrors = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasValidationErrors));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _saveButtonText;
        public string SaveButtonText
        {
            get => _saveButtonText;
            set
            {
                _saveButtonText = value;
                OnPropertyChanged();
            }
        }

        private bool _isAddingDoctor;
        public bool IsAddingDoctor
        {
            get => _isAddingDoctor;
            set
            {
                _isAddingDoctor = value;
                OnPropertyChanged();
            }
        }

        // Computed properties
        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool HasValidationErrors => ValidationErrors?.Count > 0;
        public bool IsEditingExistingDoctor => _originalDoctor != null; // Dodatkowa właściwość dla jasności

        // Commands
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        private void ValidateAndUpdateErrors()
        {
            var errors = new List<string>();

            if (EditingDoctor == null)
            {
                ValidationErrors = new ObservableCollection<string>(errors);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditingDoctor.Name))
                errors.Add("Doctor name is required");

            if (string.IsNullOrWhiteSpace(EditingDoctor.Surname))
                errors.Add("Doctor surname is required");

            if (string.IsNullOrWhiteSpace(EditingDoctor.Email))
                errors.Add("Email is required");
            else if (!IsValidEmail(EditingDoctor.Email))
                errors.Add("Please enter a valid email address");

            if (string.IsNullOrWhiteSpace(EditingDoctor.Specialization))
                errors.Add("Specialization is required");

            if (EditingDoctor.DateOfBirth == default(DateTime))
                errors.Add("Date of birth is required");

            if (EditingDoctor.DateOfBirth > DateTime.Now)
                errors.Add("Date of birth cannot be in the future");

            if (EditingDoctor.DateOfBirth > DateTime.Now.AddYears(-18))
                errors.Add("Doctor must be at least 18 years old");

            if (!string.IsNullOrWhiteSpace(EditingDoctor.TelephoneNumber) && EditingDoctor.TelephoneNumber.Length < 9)
                errors.Add("Phone number must be at least 9 digits");

            ValidationErrors = new ObservableCollection<string>(errors);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool CanSave()
        {
            ValidateAndUpdateErrors();
            return !HasValidationErrors;
        }

        private async Task SaveDoctorAsync()
        {
            if (!CanSave()) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                if (IsAddingDoctor)
                {
                    Trace.WriteLine("Saving NEW doctor");

                    // Check if email already exists
                    var existingDoctor = await context.Doctor
                        .FirstOrDefaultAsync(d => d.Email == EditingDoctor.Email);

                    if (existingDoctor != null)
                    {
                        MessageBox.Show("A doctor with this email already exists!", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var newDoctor = new Doctor
                    {
                        Name = EditingDoctor.Name,
                        Surname = EditingDoctor.Surname,
                        Email = EditingDoctor.Email,
                        IsActive = EditingDoctor.IsActive,
                        PasswordHash = EditingDoctor.PasswordHash ?? string.Empty,
                        TelephoneNumber = EditingDoctor.TelephoneNumber,
                        Specialization = EditingDoctor.Specialization,
                        Description = EditingDoctor.Description,
                        DateOfBirth = EditingDoctor.DateOfBirth,
                        CreatedAt = DateTime.Now,
                        LastLogin = null
                    };

                    context.Doctor.Add(newDoctor);
                    await context.SaveChangesAsync();

                    MessageBox.Show("Doctor was added successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Trace.WriteLine($"Updating EXISTING doctor with ID: {EditingDoctor.Id}");

                    var existingDoctor = await context.Doctor.FindAsync(EditingDoctor.Id);
                    if (existingDoctor != null)
                    {
                        // Check if email is being changed and if new email already exists
                        if (existingDoctor.Email != EditingDoctor.Email)
                        {
                            var emailExists = await context.Doctor
                                .AnyAsync(d => d.Email == EditingDoctor.Email && d.Id != EditingDoctor.Id);

                            if (emailExists)
                            {
                                MessageBox.Show("A doctor with this email already exists!", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }

                        existingDoctor.Name = EditingDoctor.Name;
                        existingDoctor.Surname = EditingDoctor.Surname;
                        existingDoctor.Email = EditingDoctor.Email;
                        existingDoctor.IsActive = EditingDoctor.IsActive;
                        existingDoctor.TelephoneNumber = EditingDoctor.TelephoneNumber;
                        existingDoctor.Specialization = EditingDoctor.Specialization;
                        existingDoctor.Description = EditingDoctor.Description;
                        existingDoctor.DateOfBirth = EditingDoctor.DateOfBirth;

                        // Don't update PasswordHash unless it's explicitly changed
                        if (!string.IsNullOrWhiteSpace(EditingDoctor.PasswordHash) &&
                            EditingDoctor.PasswordHash != existingDoctor.PasswordHash)
                        {
                            existingDoctor.PasswordHash = EditingDoctor.PasswordHash;
                        }

                        await context.SaveChangesAsync();

                        MessageBox.Show("Doctor was updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Doctor not found in database!", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                GoBack(null);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error saving doctor: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Trace.TraceError($"Inner exception: {ex.InnerException.Message}");
                }
                MessageBox.Show($"An error occurred while saving: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object parameter)
        {
            var result = MessageBox.Show("Are you sure you want to cancel? Unsaved changes will be lost.",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                GoBack(null);
            }
        }

        private void GoBack(object parameter)
        {
            _navigationService.NavigateTo<DoctorListViewModel>();
        }
    }
}