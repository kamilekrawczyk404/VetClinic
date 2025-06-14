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
        DependencyProperty.Register(
            nameof(Statuses),
            typeof(IEnumerable<string>),
            typeof(AppointmentStatusComboBox),
            new PropertyMetadata(null));

        public IEnumerable<string> Statuses
        {
            get => (IEnumerable<string>)GetValue(StatusesProperty); 
            set => SetValue(StatusesProperty, value);
        }

        public static readonly DependencyProperty SelectedStatusProperty =
            DependencyProperty.Register(
                nameof(SelectedStatus), 
                typeof(string), 
                typeof(AppointmentStatusComboBox), 
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectionChanged));

        private static void OnSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (AppointmentStatusComboBox)d;
            control.PART_ComboBox.SelectedItem = (string)e.NewValue;
        }

        public string SelectedStatus
        {
            get => (string)GetValue(SelectedStatusProperty);
            set => SetValue(SelectedStatusProperty, value);
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
