using System;
using System.Globalization;
using System.Windows.Data;

namespace VetClinic.Utils
{
    public class StarRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rating && parameter is string starPositionStr)
            {
                if (int.TryParse(starPositionStr, out int starPosition))
                {
                    return rating >= starPosition; // True jeśli gwiazdka powinna być złota
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}