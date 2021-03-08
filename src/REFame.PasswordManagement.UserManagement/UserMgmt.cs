using System.Threading.Tasks;
using REFame.PasswordManagement.UserManagement.Contracts;
using REFame.PasswordManagement.UserManagement.UI;

namespace REFame.PasswordManagement.UserManagement
{
    public class UserMgmt : IUserMgmt
    {
        public Task Open()
        {
            UserManagementView view = new UserManagementView();
            view.Show();

            return Task.CompletedTask;
        }
    }
}