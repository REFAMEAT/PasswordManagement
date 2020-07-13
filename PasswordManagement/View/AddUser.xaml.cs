using PasswordManagement.Backend;
using PasswordManagement.Database.Model;
using PasswordManagement.ViewModel;
using System;
using System.Windows;
using System.Windows.Media;
using PasswordManagement.Backend.Security;

namespace PasswordManagement.View
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            bool validInput = passwordBox.Password == repeatPasswordBox.Password;
            ((AddUserViewModel) DataContext).InputOk = validInput;

            passwordBox.Foreground = validInput ? Brushes.Green : Brushes.Red;
            repeatPasswordBox.Foreground = validInput ? Brushes.Green : Brushes.Red;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static USERDATA CreateUser()
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();

            string userName = ((LoginViewModel) addUser.DataContext).UserName;
            string password = addUser.passwordBox.Password;

            return UserFactory.CreateUser(userName, password);
        }
    }
}
