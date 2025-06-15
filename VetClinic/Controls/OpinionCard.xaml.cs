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
using VetClinic.MVVM.ViewModel.Dashboard;

namespace VetClinic.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy OpinionCard.xaml
    /// </summary>
    public partial class OpinionCard : UserControl
    {
        public static readonly DependencyProperty OpinionProperty =
              DependencyProperty.Register(
                  nameof(Opinion),
                  typeof(DetailedOpinion),
                  typeof(OpinionCard),
                  new PropertyMetadata(null));

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(
                nameof(ClickCommand),
                typeof(ICommand),
                typeof(OpinionCard),
                new PropertyMetadata(null));

        public DetailedOpinion Opinion
        {
            get => (DetailedOpinion)GetValue(OpinionProperty);
            set => SetValue(OpinionProperty, value);
        }

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        public OpinionCard()
        {
            InitializeComponent();
        }
    }
}
