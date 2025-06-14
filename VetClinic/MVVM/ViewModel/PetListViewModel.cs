using Microsoft.EntityFrameworkCore;
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
    public class PetListViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        public PetListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            Pets = new ObservableCollection<Pet>();
            AvailableOwners = new ObservableCollection<User>();

            // Commands
            AddPetCommand = new RelayCommand(AddPet, _ => CanManagePets);
            EditPetCommand = new RelayCommand(EditPet, _ => CanManagePets);
            DeletePetCommand = new RelayCommand(StartDeletePet, _ => CanManagePets);
            ViewPetDetailsCommand = new RelayCommand(ViewPetDetails);

            // Modal commands
            SavePetCommand = new RelayCommand(async _ => await SavePet(), _ => CanSavePet());
            CancelEditCommand = new RelayCommand(CancelEdit);
            ConfirmDeleteCommand = new RelayCommand(async _ => await ConfirmDelete());
            CancelDeleteCommand = new RelayCommand(CancelDelete);

            _userSessionService.UserChanged += async () => await OnUserChanged();
            _ = LoadPetsAsync();
        }

        private ObservableCollection<Pet> _pets;
        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                OnPropertyChanged();
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

        // Modal properties
        private bool _isEditingPet;
        public bool IsEditingPet
        {
            get => _isEditingPet;
            set
            {
                _isEditingPet = value;
                OnPropertyChanged();
            }
        }

        private bool _isConfirmingDelete;
        public bool IsConfirmingDelete
        {
            get => _isConfirmingDelete;
            set
            {
                _isConfirmingDelete = value;
                OnPropertyChanged();
            }
        }

        private Pet _editingPet;
        public Pet EditingPet
        {
            get => _editingPet;
            set
            {
                _editingPet = value;
                OnPropertyChanged();
            }
        }

        private Pet _petToDelete;
        public Pet PetToDelete
        {
            get => _petToDelete;
            set
            {
                _petToDelete = value;
                OnPropertyChanged();
            }
        }

        private string _editingPetTitle;
        public string EditingPetTitle
        {
            get => _editingPetTitle;
            set
            {
                _editingPetTitle = value;
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
                OnPropertyChanged(nameof(ShowOwnerSelection));
            }
        }

        public bool ShowOwnerSelection => IsAdmin && IsAddingPet;

        // User properties
        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool IsClient => _userSessionService.IsClient;
        public bool CanManagePets => IsAdmin || IsClient;

        // Commands
        public RelayCommand AddPetCommand { get; }
        public RelayCommand EditPetCommand { get; }
        public RelayCommand DeletePetCommand { get; }
        public RelayCommand ViewPetDetailsCommand { get; }
        public RelayCommand SavePetCommand { get; }
        public RelayCommand CancelEditCommand { get; }
        public RelayCommand ConfirmDeleteCommand { get; }
        public RelayCommand CancelDeleteCommand { get; }

        private async Task OnUserChanged()
        {
            await LoadPetsAsync();
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsClient));
            OnPropertyChanged(nameof(CanManagePets));
        }

        private async Task LoadPetsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                IQueryable<Pet> query = context.Pet
                    .Include(p => p.User)
                    .OrderBy(p => p.Name);

                // Jeśli użytkownik jest klientem, pokazujemy tylko jego zwierzęta
                if (IsClient && !IsAdmin)
                {
                    var currentUserId = _userSessionService.LoggedInUser?.Id;
                    if (currentUserId.HasValue)
                    {
                        query = query.Where(p => p.UserId == currentUserId.Value);
                    }
                }

                var pets = await query.ToListAsync();
                Pets = new ObservableCollection<Pet>(pets);

                Trace.WriteLine($"Loaded {pets.Count} pets from database");
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch pets: {ex.Message}");
                Trace.TraceError($"Stack trace: {ex.StackTrace}");
                Pets = new ObservableCollection<Pet>();
            }
        }

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

        private async void AddPet(object obj)
        {
            if (!CanManagePets) return;

            IsAddingPet = true;
            EditingPetTitle = "Dodaj nowe zwierzę";

            EditingPet = new Pet
            {
                DateOfBirth = DateTime.Now.AddYears(-1),
                CreatedAt = DateTime.Now,
                Gender = "Samiec",
                Species = "Pies"
            };

            // Jeśli to klient, automatycznie przypisz go jako właściciela
            if (IsClient && !IsAdmin)
            {
                EditingPet.UserId = _userSessionService.LoggedInUser.Id;
                EditingPet.User = _userSessionService.LoggedInUser;
            }
            else if (IsAdmin)
            {
                await LoadAvailableOwnersAsync();
            }

            IsEditingPet = true;
        }

        private async void EditPet(object obj)
        {
            if (!(obj is Pet pet)) return;
            if (!CanManagePets) return;

            // Dodatkowa weryfikacja dla klientów - mogą edytować tylko swoje zwierzęta
            if (IsClient && !IsAdmin && _userSessionService.LoggedInUser?.Id != pet.UserId)
                return;

            IsAddingPet = false;
            EditingPetTitle = $"Edytuj zwierzę: {pet.Name}";

            // Utwórz kopię do edycji
            EditingPet = new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                Weight = pet.Weight,
                Gender = pet.Gender,
                DateOfBirth = pet.DateOfBirth,
                UserId = pet.UserId,
                User = pet.User,
                CreatedAt = pet.CreatedAt
            };

            if (IsAdmin)
            {
                await LoadAvailableOwnersAsync();
            }

            IsEditingPet = true;
        }

        private void StartDeletePet(object obj)
        {
            if (!(obj is Pet pet)) return;
            if (!CanManagePets) return;

            // Dodatkowa weryfikacja dla klientów - mogą usuwać tylko swoje zwierzęta
            if (IsClient && !IsAdmin && _userSessionService.LoggedInUser?.Id != pet.UserId)
                return;

            PetToDelete = pet;
            IsConfirmingDelete = true;
        }

        private void ViewPetDetails(object obj)
        {
            if (!(obj is Pet pet)) return;
            // Implementacja wyświetlania szczegółów zwierzęcia
            // _navigationService.NavigateTo<PetDetailsViewModel>(pet);
        }

        private async Task SavePet()
        {
            if (EditingPet == null) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                if (IsAddingPet)
                {
                    // Dodawanie nowego zwierzęcia
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

                    // Ustaw właściciela
                    if (IsAdmin && EditingPet.User != null)
                    {
                        newPet.UserId = EditingPet.User.Id;
                    }
                    else if (IsClient && !IsAdmin)
                    {
                        newPet.UserId = _userSessionService.LoggedInUser.Id;
                    }

                    // WAŻNE: Nie dodawaj obiektu User do kontekstu - tylko UserId
                    context.Pet.Add(newPet);
                    await context.SaveChangesAsync();

                    // Przeładuj listę zwierząt
                    await LoadPetsAsync();
                }
                else
                {
                    // Edycja istniejącego zwierzęcia
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
                        await LoadPetsAsync();
                    }
                }

                IsEditingPet = false;
                EditingPet = null;
                IsAddingPet = false;
                MessageBox.Show("Zwierzę zostało zapisane pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error saving pet: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Trace.TraceError($"Inner exception: {ex.InnerException.Message}");
                }
                MessageBox.Show($"Wystąpił błąd podczas zapisywania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelEdit(object obj)
        {
            IsEditingPet = false;
            EditingPet = null;
            IsAddingPet = false;
        }

        private async Task ConfirmDelete()
        {
            if (PetToDelete == null) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                var petToRemove = await context.Pet.FindAsync(PetToDelete.Id);
                if (petToRemove != null)
                {
                    // Sprawdź czy zwierzę ma jakieś wizyty
                    var hasAppointments = await context.Appointment.AnyAsync(a => a.PetId == petToRemove.Id);
                    if (hasAppointments)
                    {
                        MessageBox.Show("Nie można usunąć zwierzęcia, które ma zaplanowane lub przeszłe wizyty.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                        IsConfirmingDelete = false;
                        PetToDelete = null;
                        return;
                    }

                    context.Pet.Remove(petToRemove);
                    await context.SaveChangesAsync();

                    // Usuń z kolekcji
                    Pets.Remove(PetToDelete);

                    MessageBox.Show("Zwierzę zostało usunięte pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error deleting pet: {ex.Message}");
                MessageBox.Show($"Wystąpił błąd podczas usuwania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsConfirmingDelete = false;
                PetToDelete = null;
            }
        }

        private void CancelDelete(object obj)
        {
            IsConfirmingDelete = false;
            PetToDelete = null;
        }

        private bool CanSavePet()
        {
            if (EditingPet == null) return false;

            var hasBasicData = !string.IsNullOrWhiteSpace(EditingPet.Name) &&
                              !string.IsNullOrWhiteSpace(EditingPet.Species) &&
                              !string.IsNullOrWhiteSpace(EditingPet.Breed) &&
                              EditingPet.Weight > 0 &&
                              !string.IsNullOrWhiteSpace(EditingPet.Gender) &&
                              EditingPet.DateOfBirth != default(DateTime);

            if (!hasBasicData) return false;

            // Sprawdź czy właściciel jest wybrany (tylko dla adminów dodających nowe zwierzę)
            if (IsAddingPet && IsAdmin)
            {
                return EditingPet.User != null;
            }

            // Dla klientów lub edycji - zawsze OK jeśli podstawowe dane są wypełnione
            return true;
        }

        public async Task RefreshPetsAsync()
        {
            await LoadPetsAsync();
        }
    }
}