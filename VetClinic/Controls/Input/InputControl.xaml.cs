﻿using System;
using System.Collections.Generic;
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
    public partial class InputControl : UserControl
    {
        public InputControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                "Placeholder",
                typeof(string),
                typeof(InputControl),
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
                typeof(InputControl),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(InputControl),
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
                typeof(InputControl),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnErrorMessageChanged));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputControl control = (InputControl)d;
            control.UpdatePlaceholderVisibility();
            control.PART_TextBox.Text = (string)e.NewValue;
        }

        private static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (InputControl)d;
            control.UpdateErrorVisibility();
            control.PART_ErrorMessageTextBlock.Text = (string)e.NewValue;
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
            if (PART_Placeholder != null && PART_TextBox != null)
            {
                PART_Placeholder.Visibility = string.IsNullOrEmpty(PART_TextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            if (ErrorMessage.Length > 0)
            {
                UpdateErrorVisibility();
            }
        }

        private void PART_TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
            UpdateErrorVisibility();
        }
    }
}
