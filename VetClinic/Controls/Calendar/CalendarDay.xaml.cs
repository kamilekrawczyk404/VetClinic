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

namespace VetClinic.Controls.Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy CalendarDay.xaml
    /// </summary>
    public partial class CalendarDay : UserControl
    {
        public CalendarDay()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsSelectedProperty =
           DependencyProperty.Register(
               nameof(IsSelected),
               typeof(bool),
               typeof(CalendarDay),
               new PropertyMetadata(false));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register(
                nameof(Date),
                typeof(DateTime),
                typeof(CalendarDay),
                new PropertyMetadata(DateTime.Now));

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CalendarDay));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(CalendarDay));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}
