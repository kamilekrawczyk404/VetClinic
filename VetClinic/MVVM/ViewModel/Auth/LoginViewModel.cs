using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Auth
{
    public class LoginViewModel : ViewModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _emailErrorMessage;
        public string EmailErrorMessage
        {
            get => _emailErrorMessage;
            set
            {
                _emailErrorMessage = value;
                OnPropertyChanged(nameof(EmailErrorMessage));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _passwordErrorMessage;
        public string PasswordErrorMessage
        {
            get => _passwordErrorMessage;
            set
            {
                _passwordErrorMessage = value;
                OnPropertyChanged(nameof(PasswordErrorMessage));
            }
        }

        private INavigationService _navigation;
        private IUserSessionService _userSessionService;
        private UserService _userService;

        public RelayCommand NavigateToRegisterViewCommand { get; }
        public AsyncRelayCommand SubmitCommand { get; }

        public LoginViewModel(UserService userService, INavigationService navigation, IUserSessionService userSessionService)
        {
            _userService = userService;
            _navigation = navigation;
            _userSessionService = userSessionService;

            PasswordErrorMessage = EmailErrorMessage = string.Empty;

            NavigateToRegisterViewCommand = new RelayCommand(NavigateToRegisterView);
            SubmitCommand = new AsyncRelayCommand(Submit);
        }
        private void NavigateToRegisterView(object obj)
        {
            _navigation.NavigateTo<RegisterViewModel>();
        }

        private async Task Submit(object obj)
        {
            EmailErrorMessage = PasswordErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    EmailErrorMessage = "This field is required.";
                }
                if (string.IsNullOrWhiteSpace(Password))
                {
                    PasswordErrorMessage = "This field is required.";
                }
                return;
            }

            var user = await _userService.LoginUserAsync(Email, Password);
                
            if (user == null)
            {
                PasswordErrorMessage = EmailErrorMessage = "Invalid email or password.";
                return;
            }

            // Navigate user to his dashboard

            _userSessionService.SetUser(user);
            _navigation.NavigateTo<DashboardViewModel>();
        }
    }
}
