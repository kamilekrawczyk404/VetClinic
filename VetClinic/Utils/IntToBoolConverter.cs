using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VetClinic.Utils
{
    class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rating && parameter is string paramStr && int.TryParse(paramStr, out int targetRating))
            {
                return rating == targetRating;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter is string paramStr && int.TryParse(paramStr, out int rating))
            {
                return rating;
            }
            return Binding.DoNothing;
        }
    }
}
