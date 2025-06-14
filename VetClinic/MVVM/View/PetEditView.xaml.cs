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
    /// Logika interakcji dla klasy PetEditView.xaml
    /// </summary>
    public partial class PetEditView : UserControl
    {
        public PetEditView()
        {
            InitializeComponent();
          
            this.Focusable = true;
        }

      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
    }
}
