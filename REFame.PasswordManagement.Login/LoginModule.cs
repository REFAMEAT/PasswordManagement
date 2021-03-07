using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Login
{
    public class LoginModule : IModule
    {
        public async Task Initialize(ICore appCore)
        {
            bool useDatabase =(await appCore
                .GetRegisteredType<IConfigurationFactory<DatabaseData>>()
                .SetPath()
                .Create()
                .LoadAsync()).UseDatabase;

            if (useDatabase)
            {
                appCore.RegisterType<ILogin, DatabaseLogin>();
            }
            else
            {
                appCore.RegisterType<ILogin, LocalLogin>();
            }

        }
    }
}