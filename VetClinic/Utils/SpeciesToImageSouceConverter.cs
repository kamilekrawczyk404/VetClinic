using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace VetClinic.Utils
{
    public class SpeciesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var defaultUri = new Uri("pack://application:,,,/VetClinic;component/Images/Species/hamster.png");
            if (value is string species && !string.IsNullOrWhiteSpace(species))
            {
                var uri = new Uri($"pack://application:,,,/VetClinic;component/Images/Species/{species.ToLower()}.png");
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = uri;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    if (bitmap.PixelWidth > 0)
                        return bitmap;
                }
                catch
                {
                }
            }
            return new BitmapImage(defaultUri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
