using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using VetClinic.Models;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel.Auth
{
    public class RegisterViewModel : ViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _nameErrorMessage;
        public string NameErrorMessage
        {
            get => _nameErrorMessage;
            set
            {
                _nameErrorMessage = value;
                OnPropertyChanged(nameof(NameErrorMessage));
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _surnameErrorMessage;
        public string SurnameErrorMessage
        {
            get => _surnameErrorMessage;
            set
            {
                _surnameErrorMessage = value;
                OnPropertyChanged(nameof(SurnameErrorMessage));
            }
        }

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

        private string _telephone;
        public string Telephone
        {
            get => _telephone;
            set
            {
                _telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }
        private string _telephoneErrorMessage;
        public string TelephoneErrorMessage
        {
            get => _telephoneErrorMessage;
            set
            {
                _telephoneErrorMessage = value;
                OnPropertyChanged(nameof(TelephoneErrorMessage));
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private string _confirmPasswordErrorMessage;
        public string ConfirmPasswordErrorMessage
        {
            get => _confirmPasswordErrorMessage;
            set
            {
                _confirmPasswordErrorMessage = value;
                OnPropertyChanged(nameof(ConfirmPasswordErrorMessage));
            }
        }

        public RelayCommand NavigateToLoginViewCommand { get; }
        public AsyncRelayCommand SubmitCommand { get; }

        private INavigationService _navigation;
        private UserService _userService;

        public RegisterViewModel(INavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;

            // Initialize all errors with empty string
            NameErrorMessage = SurnameErrorMessage = EmailErrorMessage = TelephoneErrorMessage = PasswordErrorMessage = ConfirmPasswordErrorMessage = string.Empty;
            
            NavigateToLoginViewCommand = new RelayCommand(NavigateToLoginView);
            SubmitCommand = new AsyncRelayCommand(Submit);
        }

        private async Task Submit(object obj)
        {
            bool canSubmit = await CanSubmit(obj);
            if (!canSubmit)
            {
                return;
            }

            User newClient = await _userService.CreateClientAsync(Name, Surname, Email, Telephone, Password);

            // after successful registration navigate to login view and show success message

            _navigation.NavigateTo<LoginViewModel>();
        }

        private async Task<bool> CanSubmit(object obj)
        {
            ValidateName();
            ValidateSurname();
            ValidateTelephone();
            ValidatePassword();
            ValidateConfirmPassword();

            await ValidateEmail();

            return string.IsNullOrWhiteSpace(NameErrorMessage) &&
                   string.IsNullOrWhiteSpace(SurnameErrorMessage) &&
                   string.IsNullOrWhiteSpace(EmailErrorMessage) &&
                   string.IsNullOrWhiteSpace(TelephoneErrorMessage) &&
                   string.IsNullOrWhiteSpace(PasswordErrorMessage) &&
                   string.IsNullOrWhiteSpace(ConfirmPasswordErrorMessage);
        }

        private void NavigateToLoginView(object obj)
        {
            _navigation.NavigateTo<LoginViewModel>();
        }

        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameErrorMessage = "This field is required.";
            }
            else
            {
                NameErrorMessage = string.Empty;
            }
        }

        private void ValidateSurname()
        {
            if (string.IsNullOrWhiteSpace(Surname))
            {
                SurnameErrorMessage = "This field is required.";
            }
            else
            {
                SurnameErrorMessage = string.Empty;
            }
        }

        private async Task ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailErrorMessage = "Email cannot be empty.";
            }
            // checking email using regex expression
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailErrorMessage = "This field is required.";
            }
            else if (Email.Length < 5)
            {
                EmailErrorMessage = "Email is too short. Min length is 5 symbols.";
            }
            else if (Email.Length > 50)
            {
                EmailErrorMessage = "Email is too long. Max length is 50 symbols.";
            }

            bool isEmailUnique = await _userService.IsEmailUniqueAsync(Email);

            if (!isEmailUnique)
            {
                EmailErrorMessage = "This email is already used.";
            }

            else
            {
                EmailErrorMessage = string.Empty;
            }
        }

        private void ValidateTelephone()
        {
            if (string.IsNullOrWhiteSpace(Telephone))
            {
                TelephoneErrorMessage = "This field is required.";
            }
            else if (!Regex.IsMatch(Telephone, @"^\d{9}$"))
            {
                TelephoneErrorMessage = "Telephone is not valid. Format: XXXXXXXXX";
            }
            else
            {
                TelephoneErrorMessage = string.Empty;
            }
        }

        private void ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordErrorMessage = "This field is required.";
            }
            else if (Password.Length < 8)
            {
                PasswordErrorMessage = "Password is too short. Min length is 8 symbols.";
            }
            else if (Password.Length > 50)
            {
                PasswordErrorMessage = "Password is too long. Max length is 50 symbols";
            }
            else
            {
                PasswordErrorMessage = string.Empty;
            }
        }

        private void ValidateConfirmPassword()
        {
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ConfirmPasswordErrorMessage = "This field is required.";
            }
            else if (ConfirmPassword != Password)
            {
                ConfirmPasswordErrorMessage = "Passwords do not match.";
            }
            else
            {
                ConfirmPasswordErrorMessage = string.Empty;
            }
        }
    }
}
