using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using VetClinic.MVVM.Model;

namespace VetClinic.Utils
{
    public class AppointmentStatusToBrushColorConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string statusString)
            {
                switch (statusString.ToLower())
                {
                    case "scheduled":
                        return (Brush)Application.Current.FindResource("BorderStatusScheduled");
                    case "in progress":
                        return (Brush)Application.Current.FindResource("BorderStatusInProgress");
                    case "completed":
                        return (Brush)Application.Current.FindResource("BorderStatusCompleted");
                    case "cancelled":
                        return (Brush)Application.Current.FindResource("BorderStatusCanceled");
                    default:
                        return (Brush)Application.Current.FindResource("BorderStatusScheduled");
                }
            }

            return (Brush)Application.Current.FindResource("BorderStatusScheduled");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
