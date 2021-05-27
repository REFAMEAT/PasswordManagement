using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Security;
using REFame.PasswordManagement.UserManagement.Contracts;
using REFame.PasswordManagement.UserManagement.UI.View;
using REFame.PasswordManagement.UserManagement.UI.ViewModel;
using User = REFame.PasswordManagement.Login.Contracts.User;

namespace REFame.PasswordManagement.UserManagement
{
    public class NewUserService : INewUserService
    {
        public (User, string) Create()
        {
            AddUserView addUserView = new AddUserView();
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            addUserView.DataContext = addUserViewModel;

            bool? result = addUserView.ShowDialog();

            if (result == true)
            {
                string userName = addUserViewModel.UserName;
                string password = addUserView.GetPassword();

                User user = new User()
                {
                    UserName = addUserViewModel.UserName,
                    FullName = addUserViewModel.Name,
                    Title = addUserViewModel.Title,
                    EMail = addUserViewModel.EMail,
                };

                return (user, password);
            }

            return (null, null);
        }
    }
}