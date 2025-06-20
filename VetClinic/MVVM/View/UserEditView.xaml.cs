using System.Windows;
using System.Windows.Controls;
using VetClinic.MVVM.ViewModel;

namespace VetClinic.MVVM.View
{
    public partial class UserEditView : UserControl
    {
        public UserEditView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as UserEditViewModel;

            if (viewModel?.EditingUser != null && passwordBox != null)
            {
                // Only update if password is not empty
                if (!string.IsNullOrEmpty(passwordBox.Password))
                {
                    viewModel.EditingUser.PasswordHash = passwordBox.Password;
                }
                else if (viewModel.IsAddingUser)
                {
                    // For new users, empty password should clear the hash
                    viewModel.EditingUser.PasswordHash = string.Empty;
                }
                // For existing users, empty password means "don't change"
            }
        }
    }
}