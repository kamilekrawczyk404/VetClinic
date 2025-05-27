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
using VetClinic.Models;

namespace VetClinic.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy PetCard.xaml
    /// </summary>
    public partial class PetCard : UserControl
    {
        public PetCard()
        {
            InitializeComponent();
        }

        public static DependencyProperty PetProperty = DependencyProperty.Register(
                nameof(Pet),
                typeof(object),
                typeof(PetCard),
                new PropertyMetadata(new Pet()));

        public Pet Pet
        {
            get { return (Pet)GetValue(PetProperty);  }
            set { SetValue(PetProperty, value); }
        }
    }
}
