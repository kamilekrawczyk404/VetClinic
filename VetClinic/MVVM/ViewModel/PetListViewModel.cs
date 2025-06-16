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

            NavigateToAddPetCommand = new RelayCommand(NavigateToAddPet, _ => CanManagePets);
            NavigateToEditPetCommand = new RelayCommand(NavigateToEditPet, _ => CanManagePets);
            DeletePetCommand = new RelayCommand(async pet => await DeletePet(pet), _ => CanManagePets);

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
        public bool CanManagePets => IsAdmin || IsClient;

        public RelayCommand NavigateToAddPetCommand { get; }
        public RelayCommand NavigateToEditPetCommand { get; }
        public RelayCommand DeletePetCommand { get; }
        public RelayCommand ViewPetDetailsCommand { get; }

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

                Pets = new ObservableCollection<Pet>();
            }
        }

        private void NavigateToAddPet(object obj)
        {
            if (!CanManagePets) return;

            _navigationService.NavigateTo<PetEditViewModel>();
        }

        private void NavigateToEditPet(object obj)
        {
            if (!(obj is Pet pet)) return;
            if (!CanManagePets) return;

            if (IsClient && !IsAdmin && _userSessionService.LoggedInUser?.Id != pet.UserId)
                return;

            _navigationService.NavigateTo<PetEditViewModel>(pet);
        }

        private async Task DeletePet(object obj)
        {
            if (!(obj is Pet pet)) return;
            if (!CanManagePets) return;

            if (IsClient && !IsAdmin && _userSessionService.LoggedInUser?.Id != pet.UserId)
                return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                var petToRemove = await context.Pet.FindAsync(pet.Id);
                if (petToRemove != null)
                {
                    var hasAppointments = await context.Appointment.AnyAsync(a => a.PetId == petToRemove.Id);
                    if (hasAppointments)
                    {
                        MessageBox.Show("You cannot delete pet thas has upcoming or past appointments.", "Eroor", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var result = MessageBox.Show(
                       $"Are you sure you want to delete this pet?",
                       "Delete pet",
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                        return;

                    context.Pet.Remove(petToRemove);
                    await context.SaveChangesAsync();

                    Pets.Remove(pet);

                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error deleting pet: {ex.Message}");
            }
        }

        public async Task RefreshPetsAsync()
        {
            await LoadPetsAsync();
        }
    }
}