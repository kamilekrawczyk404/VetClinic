using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VetClinic.Utils;

namespace VetClinic.MVVM.ViewModel
{
    class MainWindowViewModel : ViewModel
    {
        public ICommand MaximizeMinimizeCommand { get; }
        public ICommand HideCommand { get; }
        public ICommand CloseCommand { get; }

        public MainWindowViewModel()
        {
            MaximizeMinimizeCommand = new RelayCommand(MaximizeMinimizeWindow);
            HideCommand = new RelayCommand(HideWindow);
            CloseCommand = new RelayCommand(CloseWindow);
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
