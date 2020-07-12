using System.Windows;

namespace PasswordManagement.View
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            Loaded += (sender, args) => userNameTextBox.Focus();
        }
    }
}
