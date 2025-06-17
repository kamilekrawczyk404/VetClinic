using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using VetClinic.MVVM.ViewModel.Auth;
using VetClinic.MVVM.ViewModel.Dashboard;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class NavigationViewModel : ViewModel
    {
        // display rest of the comnponents
        private bool _areOtherVisible;
        public bool AreOtherVisible
        {
            get => _areOtherVisible;
            set
            {
                _areOtherVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _areDrugsVisible;
        public bool AreDrugsVisible
        {
            get => _areDrugsVisible;
            set
            {
                _areDrugsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _arePetsVisible;
        public bool ArePetsVisible
        {
            get => _arePetsVisible;
            set
            {
                _arePetsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _areClientsVisible;
        public bool AreClientsVisible
        {
            get => _areClientsVisible;
            set
            {
                _areClientsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _areDoctorsVisible;
        public bool AreDoctorsVisible
        {
            get => _areDoctorsVisible;
            set
            {
                _areDoctorsVisible = value;
                OnPropertyChanged();
            }
        }


        private string _selectedViewName;
        public string SelectedViewName
        {
            get => _selectedViewName;
            set
            {
                _selectedViewName = value;
                OnPropertyChanged();
            }
        }

        private IUserSessionService _userSessionService;
        private INavigationService _navigation;

        public RelayCommand NavigateToDashboardCommand { get; }
        public RelayCommand NavigateToAppointmentsListCommand { get; }
        public RelayCommand NavigateToPetsListCommand { get; }
        public RelayCommand NavigateToDrugsListCommand { get; }
        public RelayCommand NavigateToPrescriptionsListCommand { get; }
        public RelayCommand NavigateToClientsListCommand { get; }
        public RelayCommand NavigateToDoctorsListCommand { get; }

        public NavigationViewModel(IUserSessionService userSessionService, INavigationService navigation)
        {
            _userSessionService = userSessionService;
            _navigation = navigation;

            _userSessionService.UserChanged += CheckButtonsVisibility;

            AreOtherVisible = AreClientsVisible = ArePetsVisible = AreDoctorsVisible = AreDrugsVisible = false;

            NavigateToDashboardCommand = new RelayCommand(NavigateToDashboard);
            NavigateToAppointmentsListCommand = new RelayCommand(NavigateToAppointmentsList);
            NavigateToPetsListCommand = new RelayCommand(NavigateToPetsList);
            NavigateToDrugsListCommand = new RelayCommand(NavigateToDrugsList);
            NavigateToPrescriptionsListCommand = new RelayCommand(NavigateToPrescriptionsList);
            NavigateToClientsListCommand = new RelayCommand(NavigateToClientsList);
            NavigateToDoctorsListCommand = new RelayCommand(NavigateToDoctorsList);
        }

        private void NavigateToDashboard(object obj)
        {
            if (_userSessionService.IsDoctor)
            {
                _navigation.NavigateTo<DoctorDashboardViewModel>();
            }
            else if (_userSessionService.IsAdmin)
            {
                //_navigation.NavigateTo<AdminDashboardViewModel>();
            }
            else if (_userSessionService.IsClient)
            {
                _navigation.NavigateTo<ClientDashboardViewModel>();
            }
            else
            {
                _navigation.NavigateTo<LoginViewModel>();
            }
            SelectedViewName = "Dashboard";
        }

        private void NavigateToPrescriptionsList(object obj)
        {
            _navigation.NavigateTo<PrescriptionListViewModel>();
            SelectedViewName = "Prescriptions";
        }

        private void NavigateToAppointmentsList(object obj)
        {
            _navigation.NavigateTo<AppointmentListViewModel>();
            SelectedViewName = "Appointments";
        }

        private void NavigateToDrugsList(object obj)
        {
            _navigation.NavigateTo<DrugListViewModel>();
            SelectedViewName = "Drugs";
        }

        private void NavigateToPetsList(object obj)
        {
            _navigation.NavigateTo<PetListViewModel>();
            SelectedViewName = "Pets";
        }

        private void NavigateToDoctorsList(object obj)
        {
            _navigation.NavigateTo<DoctorListViewModel>();
            SelectedViewName = "Doctors";
        }

        private void NavigateToClientsList(object obj)
        {
            //_navigation.NavigateTo<ClientsListViewModel>();
            SelectedViewName = "Clients";
        }

        private void CheckButtonsVisibility()
        {
            AreDrugsVisible = false;
            ArePetsVisible = false;
            AreClientsVisible = false;
            AreDoctorsVisible = false;

            AreOtherVisible = true;

            if (_userSessionService.IsAdmin)
            {
                AreDrugsVisible = true;
                ArePetsVisible = true;
                AreClientsVisible = true;
                AreDoctorsVisible = true;
            }
            else if (_userSessionService.IsDoctor)
            {
                AreDrugsVisible = true;
            }
            else if (_userSessionService.IsClient)
            {
                ArePetsVisible = true;
                AreDoctorsVisible = true;
            }

            SelectedViewName = "Dashboard";
        }
    }
}
