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
    public class UserListViewModel : ViewModel
    {
        private readonly IDbContextFactory<VeterinaryClinicContext> _contextFactory;
        private readonly IUserSessionService _userSessionService;
        private readonly INavigationService _navigationService;

        public UserListViewModel(IDbContextFactory<VeterinaryClinicContext> contextFactory, IUserSessionService userSessionService, INavigationService navigationService)
        {
            _contextFactory = contextFactory;
            _userSessionService = userSessionService;
            _navigationService = navigationService;

            Users = new ObservableCollection<User>();

            AddUserCommand = new RelayCommand(AddUser, _ => IsAdmin);
            EditUserCommand = new RelayCommand(EditUser, _ => IsAdmin);
            DeleteUserCommand = new RelayCommand(async user => await DeleteUser(user), _ => IsAdmin);
            ViewUserDetailsCommand = new RelayCommand(ViewUserDetails);

            _userSessionService.UserChanged += async () => await OnUserChanged();
            _ = LoadUsersAsync();
        }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdmin => _userSessionService.IsAdmin;
        public bool IsClient => _userSessionService.IsClient;

        public RelayCommand AddUserCommand { get; }
        public RelayCommand EditUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand ViewUserDetailsCommand { get; }

        private async Task OnUserChanged()
        {
            await LoadUsersAsync();
            OnPropertyChanged(nameof(IsAdmin));
            OnPropertyChanged(nameof(IsClient));
        }

        private async Task LoadUsersAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            try
            {
                Trace.WriteLine("Loading users from database...");

                var users = await context.User
                    .OrderBy(u => u.Surname)
                    .ThenBy(u => u.Name)
                    .ToListAsync();

                Users = new ObservableCollection<User>(users);

                Trace.WriteLine($"Loaded {users.Count} users from database");

                // Dodatkowe logowanie dla debugowania
                foreach (var user in users)
                {
                    Trace.WriteLine($"User: {user.Name} {user.Surname} (ID: {user.Id}, Role: {user.Role})");
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Cannot fetch users: {ex.Message}");
                Trace.TraceError($"Stack trace: {ex.StackTrace}");
                Users = new ObservableCollection<User>();
            }
        }

        private async void AddUser(object obj)
        {
            if (!IsAdmin) return;

            _navigationService.NavigateTo<UserEditViewModel>();

            // Odświeżenie listy po powrocie z edycji
            await Task.Delay(100); // Krótkie opóźnienie aby nawigacja się zakończyła
            await RefreshUsersAsync();
        }

        private async void EditUser(object obj)
        {
            if (!(obj is User user)) return;
            if (!IsAdmin) return;

            _navigationService.NavigateTo<UserEditViewModel>(user);

            // Odświeżenie listy po powrocie z edycji
            await Task.Delay(100); // Krótkie opóźnienie aby nawigacja się zakończyła
            await RefreshUsersAsync();
        }

        private async Task DeleteUser(object obj)
        {
            if (!(obj is User user)) return;
            if (!IsAdmin) return;

            using var context = _contextFactory.CreateDbContext();
            try
            {
                var userToDeactivate = await context.User.FindAsync(user.Id);
                if (userToDeactivate != null)
                {
                    // Sprawdzenie czy użytkownik ma zwierzęta lub opinię
                    var hasPets = await context.Pet.AnyAsync(p => p.UserId == userToDeactivate.Id);
                    var hasOpinions = await context.Opinion.AnyAsync(o => o.ClientId == userToDeactivate.Id);

                    //if (hasPets || hasOpinions)
                    //{
                    //    MessageBox.Show("You cannot deactivate a user that has pets or opinions.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //    return;
                    //}

                    // Sprawdzenie czy to nie jest aktualnie zalogowany użytkownik
                    if (userToDeactivate.Id == _userSessionService.LoggedInUser?.Id)
                    {
                        MessageBox.Show("You cannot deactivate your own account.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var result = MessageBox.Show(
                        $"Are you sure you want to deactivate user {user.Name} {user.Surname}?",
                        "Deactivate user",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                        return;

                    userToDeactivate.IsActive = false;
                    context.User.Update(userToDeactivate);
                    await context.SaveChangesAsync();

                    // Aktualizacja widoku (można też odświeżyć całą listę z bazy)
                    Users.Remove(user);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Error deactivating user: {ex.Message}");
                MessageBox.Show("An error occurred while deactivating the user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewUserDetails(object obj)
        {
            if (!(obj is User user))
            {
                return;
            }
        }

        public async Task RefreshUsersAsync()
        {
            await LoadUsersAsync();
        }
    }
}