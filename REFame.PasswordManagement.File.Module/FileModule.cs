using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.File.Binary.Factory;
using REFame.PasswordManagement.File.Config.Factory;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.File.Module
{
    public class FileModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<IBinaryHelperFactory, BinaryHelperFactory>();

            appCore.RegisterType<IConfigurationFactory<DatabaseData>, ConfigurationFactory<DatabaseData>>();
            appCore.RegisterType<IConfigurationFactory<ThemeData>, ConfigurationFactory<ThemeData>>();

            return Task.CompletedTask;
        }
    }
}