using REFame.PasswordManagement.Login.Contracts;

namespace REFame.PasswordManagement.Login
{
    public class UserInfo : IUserInfo
    {
        public UserInfo(User user)
        {
            User = user;
        }

        public User User { get; private set; }
    }
}