using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VetClinic.Utils
{
    public class RatioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { 
            if (value is double ratio)
            {
                double rounded = Math.Round(ratio, 2);
                if (ratio > 0)
                {
                    return "+" + rounded.ToString("0.00") + "%";
                } else
                {
                    return rounded.ToString("0.00") + "%";
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
