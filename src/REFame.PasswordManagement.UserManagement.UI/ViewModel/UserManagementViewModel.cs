using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Security;
using REFame.PasswordManagement.UserManagement.Contracts;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.UserManagement.UI.ViewModel
{
    public class UserManagementViewModel : BindableBase
    {
        private User selectedUser;
        private readonly IPwmDbContext db;
        private readonly INewUserService newUserService;

        public UserManagementViewModel(
            IPwmDbContextFactory factory, 
            INewUserService newUserService)
        {
            this.newUserService = newUserService;
            db = factory.Create();

            var users = new List<User>();
            foreach (USERDATA userdata in this.db.USERDATA)
            {
                users.Add(new User()
                {
                    UserName = Encryption.DecryptString(userdata.USUSERNAME),
                    FullName = Encryption.DecryptString(userdata.USNAME),
                });
            }

            User = new ObservableCollection<User>(users);

            AddNewUserCommand = new AsyncCommand(AddNewUser);
        }
        
        public ICommand AddNewUserCommand { get; }

        public ObservableCollection<User> User { get; }

        public User SelectedUser
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value);
        }


        private async Task AddNewUser()
        {
            (User newUser, string password) = newUserService.Create();

            if (newUser != null)
            {
                var newUserData = UserFactory.CreateUser(newUser, password);

                db.USERDATA.Add(newUserData);

                await db.SaveChangesAsync(); 

            }
        }
    }
}