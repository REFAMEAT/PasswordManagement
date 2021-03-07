using System.Collections.Generic;
using System.Collections.ObjectModel;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.UserManagement.UI.ViewModel
{
    public class UserManagementViewModel : BindableBase
    {
        private User selectedUser;

        public UserManagementViewModel()
        {
            User = new ObservableCollection<User>(new List<User>
            {
                new User {UserName = "felix.eckl", Name = "Felix Eckl"},
                new User {UserName = "hannes.pleyer", Name = "Hannes Pleyer"},
                new User {UserName = "lea.eckl", Name = "Lea Eckl"},
                new User {UserName = "jakob.eckl", Name = "Jakob Eckl"},
                new User {UserName = "alex.pleyer", Name = "Alexandra Pleyer-Missios"},
            });
        }

        public ObservableCollection<User> User { get; set; }

        public User SelectedUser
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value);
        }
    }

    public class User
    {
        public string UserName { get; set; }

        public string Name { get; set; }
    }
}