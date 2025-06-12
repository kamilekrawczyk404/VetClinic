using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using VetClinic.MVVM.Model;

namespace VetClinic.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy AppointmentStatus.xaml
    /// </summary>
    public partial class AppointmentStatusComboBox : UserControl
    {
        public AppointmentStatusComboBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StatusesProperty =
        DependencyProperty.Register(nameof(Statuses), typeof(IEnumerable<string>), typeof(AppointmentStatusComboBox), new PropertyMetadata(null));

        public IEnumerable<string> Statuses
        {
            get => (IEnumerable<string>)GetValue(StatusesProperty); 
            set => SetValue(StatusesProperty, value);
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register(nameof(Status), typeof(string), typeof(AppointmentStatusComboBox), new PropertyMetadata(null));

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(AppointmentStatusComboBox), new PropertyMetadata(false));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }
    }
}
