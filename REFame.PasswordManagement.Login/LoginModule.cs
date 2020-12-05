using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Login
{
    public class LoginModule : IModule
    {
        public async Task Initialize(ICore appCore)
        {
            bool useDatabase = 
                (await JsonHelper<DatabaseData>.GetDataAsync(new DatabaseData { UseDatabase = false })).UseDatabase;

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