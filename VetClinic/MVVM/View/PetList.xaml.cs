using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VetClinic.MVVM.ViewModel;

namespace VetClinic.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy PetList.xaml
    /// </summary>
    public partial class PetList : UserControl
    {
        public PetList()
        {
            InitializeComponent();
            this.KeyDown += PetList_KeyDown;
            this.Focusable = true;
        }

        private void PetList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                var viewModel = DataContext as PetListViewModel;
                if (viewModel != null)
                {
                    if (viewModel.IsEditingPet)
                    {
                        viewModel.CancelEditCommand.Execute(null);
                        e.Handled = true;
                    }
                    else if (viewModel.IsConfirmingDelete)
                    {
                        viewModel.CancelDeleteCommand.Execute(null);
                        e.Handled = true;
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Ustaw focus na kontrolce, aby obsługa klawiatury działała
            this.Focus();
        }
    }
}