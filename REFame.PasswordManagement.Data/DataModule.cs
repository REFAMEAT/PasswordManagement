using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Data.DataManager;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Data
{
    public class DataModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            bool useDatabase = appCore
                .GetRegisteredType<IConfigurationFactory<DatabaseData>>()
                .SetPath()
                .Create()
                .Load()
                .UseDatabase;

            appCore.RegisterType<IDataManager<PasswordData>, FileDataManager>();

            if (useDatabase)
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