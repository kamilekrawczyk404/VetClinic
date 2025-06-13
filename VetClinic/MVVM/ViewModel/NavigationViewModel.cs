using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using VetClinic.Services;

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

        public bool _areDoctorsVisible;
        public bool AreDoctorsVisible
        {
            get => _areDoctorsVisible;
            set
            {
                _areDoctorsVisible = value;
                OnPropertyChanged();
            }
        }

        private IUserSessionService _userSessionService;

        public NavigationViewModel(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            _userSessionService.UserChanged += () => CheckButtonsVisibility();
            AreOtherVisible = AreClientsVisible = ArePetsVisible = AreDoctorsVisible = AreDrugsVisible = false;
        }

        private void CheckButtonsVisibility()
        {
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
        }
    }
}
