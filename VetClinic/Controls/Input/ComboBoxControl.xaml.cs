using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
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
    public partial class ComboBoxControl : UserControl
    {
        public ComboBoxControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                "Placeholder",
                typeof(string),
                typeof(ComboBoxControl),
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
                typeof(ComboBoxControl),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ComboBoxControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ComboBoxControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register(
                "SelectedValue",
                typeof(object),
                typeof(ComboBoxControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedValueChanged));

        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register(
                "SelectedValuePath",
                typeof(string),
                typeof(ComboBoxControl),
                new PropertyMetadata(string.Empty));

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(string),
                typeof(ComboBoxControl),
                new PropertyMetadata(string.Empty));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(
                "ErrorMessage",
                typeof(string),
                typeof(ComboBoxControl),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnErrorMessageChanged));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBoxControl control = (ComboBoxControl)d;
            control.UpdatePlaceholderVisibility();
            if (control.PART_ComboBox != null)
            {
                control.PART_ComboBox.SelectedItem = e.NewValue;
            }
        }

        private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComboBoxControl control = (ComboBoxControl)d;
            control.UpdatePlaceholderVisibility();
            if (control.PART_ComboBox != null)
            {
                control.PART_ComboBox.SelectedValue = e.NewValue;
            }
        }

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ComboBoxControl)d;
            control.UpdateErrorVisibility();
            if (control.PART_ErrorMessageTextBlock != null)
            {
                control.PART_ErrorMessageTextBlock.Text = (string)e.NewValue;
            }
        }

        private void UpdateErrorVisibility()
        {
            if (PART_ErrorMessageTextBlock != null)
            {
                PART_ErrorMessageTextBlock.Visibility = ErrorMessage.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
            }

            System.Windows.Media.Brush color = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "LightGray");
            System.Windows.Media.Brush foreground = (System.Windows.Media.Brush)Application.Current.FindResource(ErrorMessage.Length > 0 ? "Red" : "Gray");

            if (PART_BorderInfo != null)
            {
                PART_BorderInfo.BorderBrush = color;
            }

            if (PART_Border != null)
            {
                PART_Border.BorderBrush = color;
            }

            if (PART_TextBlockTitle != null)
            {
                PART_TextBlockTitle.Foreground = foreground;
            }

            if (PART_Placeholder != null)
            {
                PART_Placeholder.Foreground = color;
            }
        }

        private void UpdatePlaceholderVisibility()
        {
            if (PART_Placeholder != null && PART_ComboBox != null)
            {
                PART_Placeholder.Visibility = PART_ComboBox.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void PART_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            if (ErrorMessage.Length > 0)
            {
                UpdateErrorVisibility();
            }
        }

        private void PART_ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            UpdateErrorVisibility();
        }
    }
}