using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class PetEditViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;
        private readonly Pet _originalPet;

        public PetEditViewModel(
            IDbContextFactory<VeterinaryClinicContext> contextFactory,
            IUserSessionService userSessionService,
            INavigationService navigationService,
            Pet petToEdit = null)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;
            _originalPet = petToEdit;

            AvailableOwners = new ObservableCollection<User>();
            ValidationErrors = new ObservableCollection<string>();

            InitializeCommands();
            InitializePet();
            _ = LoadDataAsync();
        }

        private void InitializeCommands()
        {
            SaveCommand = new RelayCommand(async _ => await SavePetAsync(), _ => CanSave());
            CancelCommand = new RelayCommand(Cancel);
            GoBackCommand = new RelayCommand(GoBack);
        }

        private Pet _pet;
        public Pet Pet
        {
            get => _pet;
            set
            {
                _pet = value;
                OnPropertyChanged();
                InitializePetFromProperty();
            }
        }

        private void InitializePetFromProperty()
        {
            if (_pet != null)
            {
                IsAddingPet = false;
                Title = $"Edit pet: {_pet.Name}";
                SaveButtonText = "💾 Save changes";

                EditingPet = new Pet
                {
                    Id = _pet.Id,
                    Name = _pet.Name,
                    Species = _pet.Species,
                    Breed = _pet.Breed,
                    Weight = _pet.Weight,
                    Gender = _pet.Gender,
                    DateOfBirth = _pet.DateOfBirth,
                    UserId = _pet.UserId,
                    User = _pet.User,
                    CreatedAt = _pet.CreatedAt
                };
            }
            else
            {
                InitializePet();
            }
        }

        private void InitializePet()
        {
            if (_originalPet != null)
            {
                IsAddingPet = false;
                Title = $"Edit pet: {_originalPet.Name}";
                SaveButtonText = "💾 Save changes";

                EditingPet = new Pet
                {
                    Id = _originalPet.Id,
                    Name = _originalPet.Name,
                    Species = _originalPet.Species,
                    Breed = _originalPet.Breed,
                    Weight = _originalPet.Weight,
                    Gender = _originalPet.Gender,
                    DateOfBirth = _originalPet.DateOfBirth,
                    UserId = _originalPet.UserId,
                    User = _originalPet.User,
                    CreatedAt = _originalPet.CreatedAt
                };
            }
            else
            {
                IsAddingPet = true;
                Title = "➕Add new pet";
                SaveButtonText = "➕Add pet";

                EditingPet = new Pet
                {
                    DateOfBirth = DateTime.Now.AddYears(-1),
                    CreatedAt = DateTime.Now,
                    Gender = "Male",
                    Species = "Dog",
                    Weight = 1.0
                };

                if (IsClient && !IsAdmin)
                {
                    EditingPet.UserId = _userSessionService.LoggedInUser.Id;
                    EditingPet.User = _userSessionService.LoggedInUser;
                }
            }
        }

        private async Task LoadDataAsync()
        {
            if (IsAdmin)
            {
                await LoadAvailableOwnersAsync();
            }
        }

        // Properties
        private Pet _editingPet;
        public Pet EditingPet
        {
            get => _editingPet;
            set
            {
                var oldPet = _editingPet;
                _editingPet = value;
                OnPropertyChanged();

                // Powiadom o zmianach w zagnieżdżonych właściwościach
                if (oldPet?.Species != _editingPet?.Species)
                    OnPropertyChanged(nameof(EditingPet.Species));
                if (oldPet?.Name != _editingPet?.Name)
                    OnPropertyChanged(nameof(EditingPet.Name));

                OnPropertyChanged(nameof(ShowCurrentOwner));
                ValidateAndUpdateErrors();
            }
        }

        private ObservableCollection<User> _availableOwners;
        public ObservableCollection<User> AvailableOwners
        {
            get => _availableOwners;
            set
            {
                _availableOwners = value;
                OnPropertyChanged();
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

        // Individual error messages for InputControls
        private string _nameError;
        public string NameError
        {
            get => _nameError;
            set
            {
                _nameError = value;
                OnPropertyChanged();
            }
        }

        private string _speciesError;
        public string SpeciesError
        {
            get => _speciesError;
            set
            {
                _speciesError = value;
                OnPropertyChanged();
            }
        }

        private string _breedError;
        public string BreedError
        {
            get => _breedError;
            set
            {
                _breedError = value;
                OnPropertyChanged();
            }
        }

        private string _weightError;
        public string WeightError
        {
            get => _weightError;
            set
            {
                _weightError = value;
                OnPropertyChanged();
            }
        }

        private string _genderError;
        public string GenderError
        {
            get => _genderError;
            set
            {
                _genderError = value;
                OnPropertyChanged();
            }
        }

        private string _dateOfBirthError;
        public string DateOfBirthError
        {
            get => _dateOfBirthError;
            set
            {
                _dateOfBirthError = value;
                OnPropertyChanged();
            }
        }

        private string _ownerError;
        public string OwnerError
        {
            get => _ownerError;
            set
            {
                _ownerError = value;
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

        private bool _isAddingPet;
        public bool IsAddingPet
        {
            get => _isAddingPet;
            set
            {
                _isAddingPet = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowOwnerSelection));
                OnPropertyChanged(nameof(ShowCurrentOwner));
            }
        }

        // Computed properties
        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool IsClient => _userSessionService.IsClient;
        public bool ShowOwnerSelection => IsAdmin && IsAddingPet;
        public bool ShowCurrentOwner => !IsAddingPet && EditingPet?.User != null;
        public bool HasValidationErrors => !string.IsNullOrEmpty(NameError) ||
                                         !string.IsNullOrEmpty(SpeciesError) ||
                                         !string.IsNullOrEmpty(BreedError) ||
                                         !string.IsNullOrEmpty(WeightError) ||
                                         !string.IsNullOrEmpty(GenderError) ||
                                         !string.IsNullOrEmpty(DateOfBirthError) ||
                                         !string.IsNullOrEmpty(OwnerError);

        // Commands
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand GoBackCommand { get; private set; }

        private async Task LoadAvailableOwnersAsync()
        {
            if (!IsAdmin) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                var clients = await context.User
                    .Where(u => u.Role == "Client")
                    .OrderBy(u => u.Name)
                    .ThenBy(u => u.Surname)
                    .ToListAsync();

                AvailableOwners = new ObservableCollection<User>(clients);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch available owners: {ex.Message}");
                AvailableOwners = new ObservableCollection<User>();
            }
        }

        private void ValidateAndUpdateErrors()
        {
            // Clear all error messages
            NameError = string.Empty;
            SpeciesError = string.Empty;
            BreedError = string.Empty;
            WeightError = string.Empty;
            GenderError = string.Empty;
            DateOfBirthError = string.Empty;
            OwnerError = string.Empty;

            if (EditingPet == null)
            {
                return;
            }

            // Validate individual fields and set error messages
            if (string.IsNullOrWhiteSpace(EditingPet.Name))
                NameError = " - Pet name is required";

            if (string.IsNullOrWhiteSpace(EditingPet.Species))
                SpeciesError = " - Species is required";

            if (string.IsNullOrWhiteSpace(EditingPet.Breed))
                BreedError = " - Breed is required";

            if (EditingPet.Weight <= 0)
                WeightError = " - Weight must be greater than 0";

            if (string.IsNullOrWhiteSpace(EditingPet.Gender))
                GenderError = " - Gender is required";

            if (EditingPet.DateOfBirth == default(DateTime))
                DateOfBirthError = " - Date of birth is required";
            else if (EditingPet.DateOfBirth > DateTime.Now)
                DateOfBirthError = " - Date cannot be in the future";

            if (IsAddingPet && IsAdmin && EditingPet.User == null)
                OwnerError = " - Owner must be selected";

            // Keep the old ValidationErrors for backwards compatibility if needed
            var errors = new List<string>();
            if (!string.IsNullOrEmpty(NameError)) errors.Add(NameError.Trim(' ', '-'));
            if (!string.IsNullOrEmpty(SpeciesError)) errors.Add(SpeciesError.Trim(' ', '-'));
            if (!string.IsNullOrEmpty(BreedError)) errors.Add(BreedError.Trim(' ', '-'));
            if (!string.IsNullOrEmpty(WeightError)) errors.Add(WeightError.Trim(' ', '-'));
            if (!string.IsNullOrEmpty(GenderError)) errors.Add(GenderError.Trim(' ', '-'));
            if (!string.IsNullOrEmpty(DateOfBirthError)) errors.Add(DateOfBirthError.Trim(' ', '-'));
            if (!string.IsNullOrEmpty(OwnerError)) errors.Add(OwnerError.Trim(' ', '-'));

            ValidationErrors = new ObservableCollection<string>(errors);
        }

        private bool CanSave()
        {
            ValidateAndUpdateErrors();
            return !HasValidationErrors;
        }

        private async Task SavePetAsync()
        {
            if (!CanSave()) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                if (IsAddingPet)
                {
                    var newPet = new Pet
                    {
                        Name = EditingPet.Name?.Trim(),
                        Species = EditingPet.Species?.Trim(),
                        Breed = EditingPet.Breed?.Trim(),
                        Weight = EditingPet.Weight,
                        Gender = EditingPet.Gender,
                        DateOfBirth = EditingPet.DateOfBirth,
                        CreatedAt = DateTime.Now
                    };

                    if (IsAdmin && EditingPet.User != null)
                    {
                        newPet.UserId = EditingPet.User.Id;
                    }
                    else if (IsClient && !IsAdmin)
                    {
                        newPet.UserId = _userSessionService.LoggedInUser.Id;
                    }

                    context.Pet.Add(newPet);
                    await context.SaveChangesAsync();

                    MessageBox.Show("Pet was added successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var existingPet = await context.Pet.FindAsync(EditingPet.Id);
                    if (existingPet != null)
                    {
                        existingPet.Name = EditingPet.Name?.Trim();
                        existingPet.Species = EditingPet.Species?.Trim();
                        existingPet.Breed = EditingPet.Breed?.Trim();
                        existingPet.Weight = EditingPet.Weight;
                        existingPet.Gender = EditingPet.Gender;
                        existingPet.DateOfBirth = EditingPet.DateOfBirth;

                        if (IsAdmin && EditingPet.User != null)
                        {
                            existingPet.UserId = EditingPet.User.Id;
                        }

                        await context.SaveChangesAsync();

                        MessageBox.Show("Pet was updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                GoBack(null);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error saving pet: {ex.Message}");
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
            _navigationService.NavigateTo<PetListViewModel>();
        }

        // Implementacja INotifyPropertyChanged dla pełnej obsługi data binding
        public new event PropertyChangedEventHandler PropertyChanged;

        protected new virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            base.OnPropertyChanged(propertyName);
        }
    }
}