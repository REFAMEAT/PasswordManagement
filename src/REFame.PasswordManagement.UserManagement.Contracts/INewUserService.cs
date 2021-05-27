using REFame.PasswordManagement.Login.Contracts;

namespace REFame.PasswordManagement.UserManagement.Contracts
{
    public interface INewUserService
    { 
        (User, string) Create();
    }
}