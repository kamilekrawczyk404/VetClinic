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
    public class UserEditViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;
        private  User _originalUser;

        public UserEditViewModel(
            IDbContextFactory<VeterinaryClinicContext> contextFactory,
            IUserSessionService userSessionService,
            INavigationService navigationService,
            User userToEdit = null)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;
            _originalUser = userToEdit;

            ValidationErrors = new ObservableCollection<string>();
            UserRoles = new ObservableCollection<string> { "Admin", "Client" };

            InitializeCommands();
            InitializeUser();
        }
        public void SetUserToEdit(User userToEdit)
        {
            _originalUser = userToEdit;
            InitializeUser();
        }
        private void InitializeCommands()
        {
            SaveCommand = new RelayCommand(async _ => await SaveUserAsync(), _ => CanSave());
            CancelCommand = new RelayCommand(Cancel);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }


        private void InitializeUser()
        {
            if (_originalUser != null)
            {
                // EDYCJA UŻYTKOWNIKA
                IsAddingUser = false;
                Title = $"Edit user: {_originalUser.Name} {_originalUser.Surname}";
                SaveButtonText = "💾 Save changes";

                // Ustawiamy zarówno EditingUser jak i User
                EditingUser = new User
                {
                    Id = _originalUser.Id,
                    Name = _originalUser.Name,
                    Surname = _originalUser.Surname,
                    Email = _originalUser.Email,
                    IsActive = _originalUser.IsActive,
                    PasswordHash = _originalUser.PasswordHash,
                    TelephoneNumber = _originalUser.TelephoneNumber,
                    Role = _originalUser.Role,
                    DateOfBirth = _originalUser.DateOfBirth,
                    CreatedAt = _originalUser.CreatedAt,
                    LastLogin = _originalUser.LastLogin
                };

                // Ustawiamy User na oryginalny obiekt (dla porównania)
                User = _originalUser;

                // Debug log
                Trace.WriteLine($"Initializing user for edit: {_originalUser.Name} {_originalUser.Surname} (ID: {_originalUser.Id}, Role: {_originalUser.Role})");
            }
            else
            {
                // DODAWANIE NOWEGO UŻYTKOWNIKA
                IsAddingUser = true;
                Title = "➕Add new user";
                SaveButtonText = "➕Add user";

                EditingUser = new User
                {
                    DateOfBirth = DateTime.Now.AddYears(-30),
                    CreatedAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    IsActive = true,
                    Role = "Client"
                };

                // Dla nowego użytkownika User pozostaje null
                User = null;

                Trace.WriteLine("Initializing new user");
            }
        }

        // Properties
        private User _editingUser;
        public User EditingUser
        {
            get => _editingUser;
            set
            {
                _editingUser = value;
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

        private ObservableCollection<string> _userRoles;
        public ObservableCollection<string> UserRoles
        {
            get => _userRoles;
            set
            {
                _userRoles = value;
                OnPropertyChanged();
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

        private bool _isAddingUser;
        public bool IsAddingUser
        {
            get => _isAddingUser;
            set
            {
                _isAddingUser = value;
                OnPropertyChanged();
            }
        }

        // Computed properties
        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool HasValidationErrors => ValidationErrors?.Count > 0;
        public bool IsEditingExistingUser => _originalUser != null;

        // Commands
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        private void ValidateAndUpdateErrors()
        {
            var errors = new List<string>();

            if (EditingUser == null)
            {
                ValidationErrors = new ObservableCollection<string>(errors);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditingUser.Name))
                errors.Add("User name is required");

            if (string.IsNullOrWhiteSpace(EditingUser.Surname))
                errors.Add("User surname is required");

            if (string.IsNullOrWhiteSpace(EditingUser.Email))
                errors.Add("Email is required");
            else if (!IsValidEmail(EditingUser.Email))
                errors.Add("Please enter a valid email address");

            if (string.IsNullOrWhiteSpace(EditingUser.Role))
                errors.Add("User role is required");

            if (EditingUser.DateOfBirth == default(DateTime))
                errors.Add("Date of birth is required");

            if (EditingUser.DateOfBirth > DateTime.Now)
                errors.Add("Date of birth cannot be in the future");

            if (EditingUser.DateOfBirth > DateTime.Now.AddYears(-13))
                errors.Add("User must be at least 13 years old");

            if (!string.IsNullOrWhiteSpace(EditingUser.TelephoneNumber) && EditingUser.TelephoneNumber.Length < 9)
                errors.Add("Phone number must be at least 9 digits");

            // Password validation for new users
            if (IsAddingUser && string.IsNullOrWhiteSpace(EditingUser.PasswordHash))
                errors.Add("Password is required for new users");

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

        private async Task SaveUserAsync()
        {
            if (!CanSave()) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                if (IsAddingUser)
                {
                    Trace.WriteLine("Saving NEW user");

                    // Check if email already exists
                    var existingUser = await context.User
                        .FirstOrDefaultAsync(u => u.Email == EditingUser.Email);

                    if (existingUser != null)
                    {
                        MessageBox.Show("A user with this email already exists!", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var newUser = new User
                    {
                        Name = EditingUser.Name,
                        Surname = EditingUser.Surname,
                        Email = EditingUser.Email,
                        IsActive = EditingUser.IsActive,
                        PasswordHash = EditingUser.PasswordHash ?? string.Empty,
                        TelephoneNumber = EditingUser.TelephoneNumber,
                        Role = EditingUser.Role,
                        DateOfBirth = EditingUser.DateOfBirth,
                        CreatedAt = DateTime.Now,
                        LastLogin = null
                    };

                    context.User.Add(newUser);
                    await context.SaveChangesAsync();

                    MessageBox.Show("User was added successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Trace.WriteLine($"Updating EXISTING user with ID: {EditingUser.Id}");

                    var existingUser = await context.User.FindAsync(EditingUser.Id);
                    if (existingUser != null)
                    {
                        // Check if email is being changed and if new email already exists
                        if (existingUser.Email != EditingUser.Email)
                        {
                            var emailExists = await context.User
                                .AnyAsync(u => u.Email == EditingUser.Email && u.Id != EditingUser.Id);

                            if (emailExists)
                            {
                                MessageBox.Show("A user with this email already exists!", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }

                        // Prevent user from removing their own admin rights
                        var currentUser = _userSessionService.LoggedInUser;
                        if (currentUser != null && currentUser.Id == EditingUser.Id &&
                            currentUser.Role == "Admin" && EditingUser.Role != "Admin")
                        {
                            MessageBox.Show("You cannot remove your own admin privileges!", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        existingUser.Name = EditingUser.Name;
                        existingUser.Surname = EditingUser.Surname;
                        existingUser.Email = EditingUser.Email;
                        existingUser.IsActive = EditingUser.IsActive;
                        existingUser.TelephoneNumber = EditingUser.TelephoneNumber;
                        existingUser.Role = EditingUser.Role;
                        existingUser.DateOfBirth = EditingUser.DateOfBirth;

                        // Don't update PasswordHash unless it's explicitly changed
                        if (!string.IsNullOrWhiteSpace(EditingUser.PasswordHash) &&
                            EditingUser.PasswordHash != existingUser.PasswordHash)
                        {
                            existingUser.PasswordHash = EditingUser.PasswordHash;
                        }

                        await context.SaveChangesAsync();

                        MessageBox.Show("User was updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("User not found in database!", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                GoBack(null);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error saving user: {ex.Message}");
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
            _navigationService.NavigateTo<UserListViewModel>();
        }
    }
}