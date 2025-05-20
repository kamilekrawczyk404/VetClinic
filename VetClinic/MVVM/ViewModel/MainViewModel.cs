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

        public ICommand MaximizeMinimizeCommand { get; }
        public ICommand HideCommand { get; }
        public ICommand CloseCommand { get; }

        public MainViewModel(INavigationService navigation)
        {
            MaximizeMinimizeCommand = new RelayCommand(MaximizeMinimizeWindow);
            HideCommand = new RelayCommand(HideWindow);
            CloseCommand = new RelayCommand(CloseWindow);

            _navigation = navigation;

            _navigation.NavigateTo<LoginViewModel>();

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
