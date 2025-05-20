using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VetClinic.Utils
{
    // class for adding image path to the button as a property (changing the image in the button while it is hovered)
    public static class ButtonAttachedProperties
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.RegisterAttached(
                "ImageSource",
                typeof(ImageSource),
                typeof(ButtonAttachedProperties),
                new PropertyMetadata(null));

        public static ImageSource GetImageSource(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageSourceProperty);
        }

        public static void SetImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty ActiveImageSourceProperty =
            DependencyProperty.RegisterAttached(
                "ActiveImageSource",
                typeof(ImageSource),
                typeof(ButtonAttachedProperties),
                new PropertyMetadata(null));

        public static ImageSource GetActiveImageSource(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ActiveImageSourceProperty);
        }

        public static void SetActiveImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ActiveImageSourceProperty, value);
        }
    }
}
