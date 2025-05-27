using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VetClinic.Controls.Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy WeekCalendar.xaml
    /// </summary>
    public partial class CalendarWeek : UserControl
    {
        public CalendarWeek()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(CalendarWeek), new PropertyMetadata(DateTime.Now));

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public static readonly DependencyProperty DaysProperty =
            DependencyProperty.Register("Days", typeof(object), typeof(CalendarWeek), new PropertyMetadata(null));

        public object Days
        {
            get { return GetValue(DaysProperty); }
            set { SetValue(DaysProperty, value); }
        }

        public static readonly DependencyProperty OnDaySelectedProperty =
            DependencyProperty.Register(nameof(OnDaySelected), typeof(ICommand), typeof(CalendarWeek));

        public ICommand OnDaySelected
        {
            get => (ICommand)GetValue(OnDaySelectedProperty);
            set => SetValue(OnDaySelectedProperty, value);
        }
    }
}
