using System.Windows;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(
            ILogin logonMehtod,
            IConfigurationFactory<DatabaseData> configurationFactory)
        {
            InitializeComponent();

            DataContext = new LoginViewModel(logonMehtod, configurationFactory);

            // Focus username text-box on open
            Loaded += (sender, args) => userNameTextBox.Focus();
        }
    }
}