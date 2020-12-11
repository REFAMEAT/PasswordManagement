using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Data.DataManager;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Data
{
    public class DataModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            if (Globals.UseDatabase)
            {
                appCore.RegisterType<IDataManager<PasswordData>, DatabaseDataManager>();
            }
            else
            {
                appCore.RegisterType<IDataManager<PasswordData>, FileDataManager>();
            }

            return Task.CompletedTask;
        }
    }
}