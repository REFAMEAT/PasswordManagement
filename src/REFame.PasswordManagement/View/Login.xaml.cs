using System.Windows;
using System.Windows.Media;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.Login.Contracts;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(ILogin logonMehtod)
        {
            InitializeComponent();

            DataContext = new LoginViewModel(logonMehtod, SetPasswordState);

            // Focus username text-box on open
            Loaded += (sender, args) => userNameTextBox.Focus();
        }

        private void SetPasswordState()
        {
            passwordBox.Foreground = Brushes.Red;
            userNameTextBox.Foreground = Brushes.Red;
        }
    }
}