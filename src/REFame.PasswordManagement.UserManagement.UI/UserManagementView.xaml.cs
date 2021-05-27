using System.Windows;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.UserManagement.Contracts;
using REFame.PasswordManagement.UserManagement.UI.ViewModel;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.UserManagement.UI
{
    /// <summary>
    /// Interaction logic for UserManagementView.xaml
    /// </summary>
    public partial class UserManagementView : PwmWindow
    {
        public UserManagementView(
            IPwmDbContextFactory factory,
            INewUserService newUserService)
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
            DataContext = new UserManagementViewModel(factory, newUserService);
        }
    }
}
