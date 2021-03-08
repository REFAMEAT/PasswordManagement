using System.Windows;
using System.Windows.Media;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaction logic for AddUser.xaml
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

        internal static USERDATA CreateUser(bool shutDownAppOnCancel = false)
        {
            var addUser = new AddUser();
            bool? result = addUser.ShowDialog();

            if (result == true)
            {
                string userName = ((AddUserViewModel) addUser.DataContext).UserName;
                string password = addUser.passwordBox.Password;

                return UserFactory.CreateUser(userName, password);
            }

            if (shutDownAppOnCancel)
            {
                Application.Current.Shutdown();
            }

            return null;
        }
    }
}