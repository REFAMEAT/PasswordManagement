using System.Windows;
using System.Windows.Media;
using REFame.PasswordManagement.UserManagement.UI.ViewModel;

namespace REFame.PasswordManagement.UserManagement.UI.View
{
    /// <summary>
    /// Interaction logic for AddUserView.xaml
    /// </summary>
    public partial class AddUserView : Window
    {
        private Brush defaultBrush;

        public AddUserView()
        {
            InitializeComponent();

            this.defaultBrush = nameTextBox.Foreground;
        }

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            bool passwordValid = !string.IsNullOrEmpty(passwordBox.Password) 
                                 && !string.IsNullOrEmpty(repeatPasswordBox.Password)
                                 && passwordBox.Password == repeatPasswordBox.Password;
            bool nameValid = !string.IsNullOrEmpty(nameTextBox.Text);
            bool userNameValid = !string.IsNullOrEmpty(userNameTextBox.Text);

            passwordBox.Foreground = passwordValid ? defaultBrush : Brushes.Red;
            repeatPasswordBox.Foreground = passwordValid ? defaultBrush : Brushes.Red;
            nameTextBox.Foreground = nameValid ? defaultBrush : Brushes.Red;
            userNameTextBox.Foreground = userNameValid ? defaultBrush : Brushes.Red;

            ((AddUserViewModel)DataContext).InputOk = passwordValid && nameValid && userNameValid;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string GetPassword()
        {
            return passwordBox.Password;
        }
    }
}
