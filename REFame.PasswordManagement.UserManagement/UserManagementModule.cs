using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.UserManagement.Contracts;

namespace REFame.PasswordManagement.UserManagement
{
    public class UserManagementModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<IUserMgmt, UserMgmt>();

            return Task.CompletedTask;
        }
    }
}