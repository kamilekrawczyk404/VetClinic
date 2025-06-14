using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

            AddPetCommand = new RelayCommand(AddPet, _ => CanManagePets);
            EditPetCommand = new RelayCommand(EditPet, _ => CanManagePets);
            DeletePetCommand = new RelayCommand(DeletePet, _ => CanManagePets);
            ViewPetDetailsCommand = new RelayCommand(ViewPetDetails);

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

        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool IsClient => _userSessionService.IsClient;
        public bool CanManagePets => IsAdmin || IsClient; // Klienci mogą zarządzać swoimi zwierzętami

        public RelayCommand AddPetCommand { get; }
        public RelayCommand EditPetCommand { get; }
        public RelayCommand DeletePetCommand { get; }
        public RelayCommand ViewPetDetailsCommand { get; }

        private async Task OnUserChanged()
        {
            await LoadPetsAsync();
        }

        private async Task LoadPetsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                IQueryable<Pet> query = context.Pet
                    .Include(p => p.User) // Dołączamy informacje o właścicielu
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

        private void AddPet(object obj)
        {
            if (!CanManagePets) return;
            // Implementacja dodawania zwierzęcia
            // _navigationService.NavigateTo<AddPetViewModel>();
        }

        private void EditPet(object obj)
        {
            if (!(obj is Pet pet)) return;
            if (!CanManagePets) return;

            // Dodatkowa weryfikacja dla klientów - mogą edytować tylko swoje zwierzęta
            if (IsClient && !IsAdmin && _userSessionService.LoggedInUser?.Id != pet.UserId)
                return;

            // Implementacja edycji zwierzęcia
            // _navigationService.NavigateTo<EditPetViewModel>(pet);
        }

        private void DeletePet(object obj)
        {
            if (!(obj is Pet pet)) return;
            if (!CanManagePets) return;

            // Dodatkowa weryfikacja dla klientów - mogą usuwać tylko swoje zwierzęta
            if (IsClient && !IsAdmin && _userSessionService.LoggedInUser?.Id != pet.UserId)
                return;

            // Implementacja usuwania zwierzęcia
            // Można dodać dialog potwierdzenia i następnie usunięcie z bazy
        }

        private void ViewPetDetails(object obj)
        {
            if (!(obj is Pet pet)) return;

            // Implementacja wyświetlania szczegółów zwierzęcia
            // _navigationService.NavigateTo<PetDetailsViewModel>(pet);
        }

        private bool CanEditPet(Pet pet)
        {
            // Logika została przeniesiona do komend - używamy CanManagePets
            // Dodatkowa weryfikacja właściciela odbywa się w komendach
            return CanManagePets;
        }

        public async Task RefreshPetsAsync()
        {
            await LoadPetsAsync();
        }
    }
}