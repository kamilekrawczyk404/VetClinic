using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace VetClinic.Utils
{
    public class TrendRatioToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double ratio)
            {
                if (ratio > 0)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Images/DashboardItem/trend-up.png"));
                }
                else if (ratio < 0)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Images/DashboardItem/trend-down.png"));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Images/DashboardItem/trend-flat.png"));
                }
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
