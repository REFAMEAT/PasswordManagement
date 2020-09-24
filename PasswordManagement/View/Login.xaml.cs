using System.Windows;
using PasswordManagement.Model.Interfaces;
using PasswordManagement.ViewModel;

namespace PasswordManagement.View
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