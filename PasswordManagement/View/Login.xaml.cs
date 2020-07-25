using System.Windows;

namespace PasswordManagement.View
{
    /// <summary>
    ///     Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        internal Login()
        {
            InitializeComponent();

            // Focus username text-box on open
            Loaded += (sender, args) => userNameTextBox.Focus();
        }
    }
}