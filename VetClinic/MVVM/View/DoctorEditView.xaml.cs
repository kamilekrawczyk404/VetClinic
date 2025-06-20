using System.Windows;
using System.Windows.Controls;
using VetClinic.MVVM.ViewModel;

namespace VetClinic.MVVM.View
{
    public partial class DoctorEditView : UserControl
    {
        public DoctorEditView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is DoctorEditViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                if (passwordBox != null && viewModel.EditingDoctor != null)
                {
                    // Only update password hash if password was actually entered
                    if (!string.IsNullOrEmpty(passwordBox.Password))
                    {
                        // In a real application, you would hash the password here
                        // For now, we'll just store it as plain text (NOT RECOMMENDED for production)
                        viewModel.EditingDoctor.PasswordHash = passwordBox.Password;
                    }
                    else if (viewModel.IsAddingDoctor)
                    {
                        // For new doctors, empty password means no password set
                        viewModel.EditingDoctor.PasswordHash = string.Empty;
                    }
                    // For existing doctors, don't change the password if field is empty
                }
            }
        }
    }
}