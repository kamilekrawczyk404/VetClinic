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

namespace VetClinic.Controls.Input
{
    /// <summary>
    /// Logika interakcji dla klasy CustomDatePicker.xaml
    /// </summary>
    public partial class CustomDatePicker : UserControl
    {
        public CustomDatePicker()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(CustomDatePicker), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(
                nameof(SelectedDate), 
                typeof(DateTime?), 
                typeof(CustomDatePicker),
                new FrameworkPropertyMetadata(DateTime.MinValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDateChanged));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(
                "ErrorMessage",
                typeof(string),
                typeof(CustomDatePicker),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnErrorMessageChanged));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }
        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CustomDatePicker)d;
            control.UpdateErrorVisibility();
            control.PART_ErrorMessageTextBlock.Text = (string)e.NewValue;
        }

        private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CustomDatePicker)d;
            control.UpdateErrorVisibility();
            control.PART_DatePicker.SelectedDate = (DateTime)e.NewValue;
        }
        private void UpdateErrorVisibility()
        {
            if (PART_ErrorMessageTextBlock != null)
            {
                //PART_ErrorMessageTextBlock.Visibility = ErrorMessage?.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
                PART_ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            }

            System.Windows.Media.Brush color = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "LightGray");
            System.Windows.Media.Brush foreground = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "Gray");

            // Change upper container border color
            if (PART_InfoBorder != null)
            {
                PART_InfoBorder.BorderBrush = color;
            }

            // Change title foreground color
            if (PART_TextBlockTitle != null) 
            {
                PART_TextBlockTitle.Foreground = foreground;
            }

            // Change main container border color
            if (PART_Border != null)
            {
                PART_Border.BorderBrush = color;
            }

            // Change placeholder foreground color
            if (PART_DatePicker != null)
            {
                PART_DatePicker.Foreground = foreground;
            }
        }

        private void PART_DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateErrorVisibility();
        }

        private void PART_DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ErrorMessage.Length > 0)
            {
                UpdateErrorVisibility();
            }
        }
    }
}
