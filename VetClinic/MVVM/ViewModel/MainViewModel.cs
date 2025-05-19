using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using VetClinic.Services;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    public class MainViewModel : ViewModel
    {
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

        public MainViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            NavigateToAuthPageCommand = new RelayCommand(o => { Navigation.NavigateTo<AuthViewModel>(); });
        }
    }
}
