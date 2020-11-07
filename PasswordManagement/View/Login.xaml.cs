using System.Windows;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        internal Login(ILogin logonMehtod)
        {
            InitializeComponent();

            DataContext = new LoginViewModel(logonMehtod);

            // Focus username text-box on open
            Loaded += (sender, args) => userNameTextBox.Focus();
        }
    }
}