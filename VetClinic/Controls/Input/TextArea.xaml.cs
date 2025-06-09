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
    /// Logika interakcji dla klasy TextArea.xaml
    /// </summary>
    public partial class TextArea : UserControl
    {
        public TextArea()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(TextArea),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                "Placeholder",
                typeof(string),
                typeof(TextArea),
                new PropertyMetadata(string.Empty, OnPlaceholderChanged));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextArea),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(
                "ErrorMessage",
                typeof(string),
                typeof(TextArea),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnErrorMessageChanged));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextArea control = (TextArea)d;
            control.UpdatePlaceholderVisibility();
            control.PART_TextArea.Text = (string)e.NewValue;
        }

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextArea control = (TextArea)d;
            control.UpdateErrorVisibility();
            control.PART_ErrorMessageTextBlock.Text = (string)e.NewValue;
        }
        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Input control = (Input)d;
            // control.PART_Placeholder.Text = (string)e.NewValue; // Not strictly needed with binding
        }

        private void UpdateErrorVisibility()
        {
            if (PART_ErrorMessageTextBlock != null)
            {
                PART_ErrorMessageTextBlock.Visibility = ErrorMessage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
            }

            System.Windows.Media.Brush color = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "LightGray");

            if (PART_TextAreaBorder != null)
            {
                PART_TextAreaBorder.BorderBrush = color;
                PART_TextAreaBorder.CornerRadius = new CornerRadius(ErrorMessage.Length == 0 ? 5 : 0, 5, 5, 5);
            }
            if (PART_TextAreaPlaceholder != null)
            {
                PART_TextAreaPlaceholder.Foreground = PART_TextAreaTitle.Foreground =  color;
            }
        }

        private void UpdatePlaceholderVisibility()
        {
            if (PART_TextAreaPlaceholder != null && PART_TextArea != null)
            {
                PART_TextAreaPlaceholder.Visibility = string.IsNullOrEmpty(PART_TextArea.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void PART_TextArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void PART_TextArea_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            UpdateErrorVisibility();
        }
    }
}
