using System.Windows;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.UserManagement.UI
{
    /// <summary>
    /// Interaction logic for UserManagementView.xaml
    /// </summary>
    public partial class UserManagementView : PwmWindow
    {
        public UserManagementView()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }
    }
}
