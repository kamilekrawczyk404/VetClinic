using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VetClinic.Controls.Input
{
    public static class PlaceholderTextInput
    {
        //public PlaceholderTextInput()
        //{
        //    InitializeComponent();
        //    PART_TextBox.TextChanged += PART_TextBox_TextChanged;
        //    PART_TextBox.Loaded += PART_TextBox_Loaded;
        //}

        //public static readonly DependencyProperty PlaceholderTextProperty =
        //    DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextInput), new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        //public string PlaceholderText
        //{
        //    get { return (string)GetValue(PlaceholderTextProperty); }
        //    set { SetValue(PlaceholderTextProperty, value); }
        //}

        //private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((PlaceholderTextInput)d).UpdatePlaceholderVisibility();
        //}

        //public static readonly DependencyProperty TextProperty =
        //    DependencyProperty.Register("Text", typeof(string), typeof(PlaceholderTextInput), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextPropertyChanged));
        //                                );
        //public string Text
        //{
        //    get { return (string)GetValue(TextProperty); }
        //    set { SetValue(TextProperty, value); }
        //}

        //private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ((PlaceholderTextInput)d).PART_TextBox.Text = (string)e.NewValue;
        //    ((PlaceholderTextInput)d).UpdatePlaceholderVisibility();
        //}

        //private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    SetValue(TextProperty, PART_TextBox.Text);
        //    UpdatePlaceholderVisibility
        //}

        //private void UpdatePlaceholderVisibility()
        //{
        //    if (string.IsNullOrEmpty(Text))
        //    {
        //        PART_Placeholder.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        PART_Placeholder.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void PART_TextBox_Loaded(object sender, RoutedEventArgs e)
        //{
        //    UpdatePlaceholderVisibility();
        //}

        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    PART_Border.BorderBrush = (SolidColorBrush)FindResource("Turqoise");
        //    PART_TextBox.Focus();
        //}

        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    PART_Border.BorderBrush = (SolidColorBrush)FindResource("Gray");
        //}
    }
}
