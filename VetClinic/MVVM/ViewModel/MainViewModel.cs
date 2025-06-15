using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using VetClinic.MVVM.ViewModel.Auth;
using VetClinic.Services;
using VetClinic.Utils;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel.Dashboard;

namespace VetClinic.MVVM.ViewModel
{
    public class MainViewModel : ViewModel
    {
        private readonly NavigationViewModel _navigationViewModel;
        private readonly IUserSessionService _userSessionService;
        public NavigationViewModel NavigationViewModel => _navigationViewModel;

        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToAuthPageCommand { get; }

        public ICommand MaximizeMinimizeCommand { get; }
        public ICommand HideCommand { get; }
        public ICommand CloseCommand { get; }

        public MainViewModel(INavigationService navigation, IUserSessionService userSessionService, VeterinaryClinicContext context)
        {
            MaximizeMinimizeCommand = new RelayCommand(MaximizeMinimizeWindow);
            HideCommand = new RelayCommand(HideWindow);
            CloseCommand = new RelayCommand(CloseWindow);

            _navigation = navigation;
            _userSessionService = userSessionService;
            _navigationViewModel = new NavigationViewModel(_userSessionService, _navigation);

             _navigation.NavigateTo<ClientDashboardViewModel>();
            // _navigation.NavigateTo<DoctorListViewModel>();
            // _navigation.NavigateTo<AppointmentListViewModel>();
            // _navigation.NavigateTo<PrescriptionListViewModel>();

            //_navigation.NavigateTo<LoginViewModel>();
            // get the user
            //User logged = context.User.FirstOrDefault(u => u.Email == ".com");
           //_userSessionService.SetUser(logged);

            Doctor logged = context.Doctor.FirstOrDefault(u => u.Email == "marek.weterynarz@vetclinic.com");
            _userSessionService.SetDoctor(logged);
            _navigation.NavigateTo<DoctorDashboardViewModel>();

            //  _navigationViewModel = navigationViewModel;

            NavigateToAuthPageCommand = new RelayCommand(o => { Navigation.NavigateTo<LoginViewModel>(); });
        }

        private void MaximizeMinimizeWindow(object o)
        {
            if (Application.Current.MainWindow != null)
            {
                if (WindowState.Maximized == Application.Current.MainWindow.WindowState)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }

        private void HideWindow(object o)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        private void CloseWindow(object o)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Close();
            }
        }
    }
}
