using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace VetClinic.Utils
{
    public class AppointmentStatusToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string statusString)
            {
                switch (statusString.ToLower())
                {
                    case "scheduled":
                        return (Brush)Application.Current.FindResource("BackgroundStatusScheduled");
                    case "in progress":
                        return (Brush)Application.Current.FindResource("BackgroundStatusInProgress");
                    case "completed":
                        return (Brush)Application.Current.FindResource("BackgroundStatusCompleted");
                    case "cancelled":
                        return (Brush)Application.Current.FindResource("BackgroundStatusCanceled");
                    default:
                        return (Brush)Application.Current.FindResource("BackgroundStatusScheduled");
                }
            }

            return (Brush)Application.Current.FindResource("BackgroundStatusScheduled");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
