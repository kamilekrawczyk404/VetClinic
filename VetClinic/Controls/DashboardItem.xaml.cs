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
using VetClinic.Controls.Input;

namespace VetClinic.Controls
{
    /// <summary>
    /// Logika interakcji dla klasy DashboardItem.xaml
    /// </summary>
    public partial class DashboardItem : UserControl
    {
        public DashboardItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached(
                "Title",
                typeof(string),
                typeof(DashboardItem),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.RegisterAttached(
                "ImageSource",
                typeof(string),
                typeof(DashboardItem),
                new PropertyMetadata(string.Empty));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(double),
                typeof(DashboardItem),
                new PropertyMetadata(null));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty RatioProperty =
            DependencyProperty.RegisterAttached(
                "Ratio",
                typeof(double),
                typeof(DashboardItem),
                new PropertyMetadata(null));

        public double Ratio
        {
            get { return (double)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }
        public static readonly DependencyProperty ShowTrendLineProperty =
          DependencyProperty.RegisterAttached(
              "ShowTrendLine",
              typeof(bool),
              typeof(DashboardItem),
              new PropertyMetadata(true));

        public bool ShowTrendLine
        {
            get { return (bool)GetValue(ShowTrendLineProperty); }
            set { SetValue(ShowTrendLineProperty, value); }
        }
    }
}
