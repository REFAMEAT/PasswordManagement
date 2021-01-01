using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.Settings.SettingFactories;
using REFame.PasswordManagement.Settings.SettingFactories.Contracts;

namespace REFame.PasswordManagement.Settings
{
    public class SettingsModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<IDatabaseSettingsFactory, DatabaseSettingsFactory>();
            appCore.RegisterType<IThemeSettingsFactory, ThemeSettingsFactory>();

            appCore.RegisterType<ISetting, Setting>();

            return Task.CompletedTask;
        }
    }
}