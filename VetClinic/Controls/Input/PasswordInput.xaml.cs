using System;
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
    /// Logika interakcji dla klasy PasswordInput.xaml
    /// </summary>
    public partial class PasswordInput : UserControl
    {
        public PasswordInput()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                "Placeholder",
                typeof(string),
                typeof(PasswordInput),
                new PropertyMetadata(string.Empty));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(PasswordInput),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(
                "Password",
                typeof(string),
                typeof(PasswordInput),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(
                "ErrorMessage",
                typeof(string),
                typeof(PasswordInput),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnErrorMessageChanged));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordInput control = (PasswordInput)d;
            control.UpdatePlaceholderVisibility();
        }

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PasswordInput)d;
            control.UpdateErrorVisibility();
        }

        private void UpdateErrorVisibility()
        {
            if (PART_ErrorMessageTextBlock != null)
            {
                PART_ErrorMessageTextBlock.Visibility = ErrorMessage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
            }

            System.Windows.Media.Brush borderColor = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "LightGray");
            System.Windows.Media.Brush foregroundColor = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "Gray");


            if (PART_BorderInfo != null)
            {
                PART_BorderInfo.BorderBrush = borderColor;
            }

            if (PART_Border != null)
            {
                PART_Border.BorderBrush = borderColor;
            }

            if (PART_TextBlockTitle != null)
            {
                PART_TextBlockTitle.Foreground = foregroundColor;
            }

            if (PART_Placeholder != null)
            {
                PART_Placeholder.Foreground = borderColor;
            }
        }

        private void UpdatePlaceholderVisibility()
        {
            if (PART_Placeholder != null && PART_PasswordBox != null)
            {
                PART_Placeholder.Visibility = string.IsNullOrEmpty(PART_PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void PART_PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            if (ErrorMessage.Length > 0)
            {
                UpdateErrorVisibility();
            }
        }

        private void PART_PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            UpdateErrorVisibility();
        }
    }
}