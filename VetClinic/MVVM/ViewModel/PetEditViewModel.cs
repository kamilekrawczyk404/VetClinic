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
    public class PetEditViewModel : ViewModel
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
                Title = $"Edytuj zwierzę: {_pet.Name}";
                SaveButtonText = "Zapisz zmiany";

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
                Title = $"Edytuj zwierzę: {_originalPet.Name}";
                SaveButtonText = "Zapisz zmiany";

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
                Title = "Dodaj nowe zwierzę";
                SaveButtonText = "Dodaj zwierzę";

                EditingPet = new Pet
                {
                    DateOfBirth = DateTime.Now.AddYears(-1),
                    CreatedAt = DateTime.Now,
                    Gender = "Male",
                    Species = "Pies",
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
                _editingPet = value;
                OnPropertyChanged();
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
        public bool HasValidationErrors => ValidationErrors?.Count > 0;

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
            var errors = new List<string>();

            if (EditingPet == null)
            {
                ValidationErrors = new ObservableCollection<string>(errors);
                return;
            }

            if (string.IsNullOrWhiteSpace(EditingPet.Name))
                errors.Add("Imię zwierzęcia jest wymagane");

            if (string.IsNullOrWhiteSpace(EditingPet.Species))
                errors.Add("Gatunek jest wymagany");

            if (string.IsNullOrWhiteSpace(EditingPet.Breed))
                errors.Add("Rasa jest wymagana");

            if (EditingPet.Weight <= 0)
                errors.Add("Waga musi być większa od 0");

            if (string.IsNullOrWhiteSpace(EditingPet.Gender))
                errors.Add("Płeć jest wymagana");

            if (EditingPet.DateOfBirth == default(DateTime))
                errors.Add("Data urodzenia jest wymagana");

            if (EditingPet.DateOfBirth > DateTime.Now)
                errors.Add("Data urodzenia nie może być w przyszłości");

            if (IsAddingPet && IsAdmin && EditingPet.User == null)
                errors.Add("Właściciel jest wymagany");

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
                        Name = EditingPet.Name,
                        Species = EditingPet.Species,
                        Breed = EditingPet.Breed,
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

                    MessageBox.Show("Zwierzę zostało dodane pomyślnie!", "Sukces",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var existingPet = await context.Pet.FindAsync(EditingPet.Id);
                    if (existingPet != null)
                    {
                        existingPet.Name = EditingPet.Name;
                        existingPet.Species = EditingPet.Species;
                        existingPet.Breed = EditingPet.Breed;
                        existingPet.Weight = EditingPet.Weight;
                        existingPet.Gender = EditingPet.Gender;
                        existingPet.DateOfBirth = EditingPet.DateOfBirth;

                        if (IsAdmin && EditingPet.User != null)
                        {
                            existingPet.UserId = EditingPet.User.Id;
                        }

                        await context.SaveChangesAsync();


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
                MessageBox.Show($"Wystąpił błąd podczas zapisywania: {ex.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object parameter)
        {
            var result = MessageBox.Show("Czy na pewno chcesz anulować? Niezapisane zmiany zostaną utracone.",
                "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                GoBack(null);
            }
        }

        private void GoBack(object parameter)
        {
            _navigationService.NavigateTo<PetListViewModel>();
        }
    }
}