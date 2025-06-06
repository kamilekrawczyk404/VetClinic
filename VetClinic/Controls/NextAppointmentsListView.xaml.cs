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
using VetClinic.Controls.Calendar;
using VetClinic.Models;
using VetClinic.MVVM.Model;

namespace VetClinic.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy NextAppointmentsListView.xaml
    /// </summary>
    public partial class NextAppointmentsListView : UserControl
    {
        public NextAppointmentsListView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(object),
                typeof(NextAppointmentsListView),
                new PropertyMetadata(null));

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty OnAppoinmentClickedProperty =
            DependencyProperty.Register(
                nameof(OnAppointmentClicked),
                typeof(ICommand),
                typeof(NextAppointmentsListView));

        public ICommand OnAppointmentClicked
        {
            get => (ICommand)GetValue(OnAppoinmentClickedProperty);
            set => SetValue(OnAppoinmentClickedProperty, value);
        }

        public static readonly DependencyProperty StatusesProperty =
            DependencyProperty.Register(
                nameof(Statuses),
                typeof(ICollection<AppointmentStatus>),
                typeof(NextAppointmentsListView));

        public ICollection<AppointmentStatus> Statuses
        {
            get => (ICollection<AppointmentStatus>)GetValue(StatusesProperty);
            set => SetValue(StatusesProperty, value);
        }
    }
}
