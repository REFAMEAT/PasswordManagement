using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Implementations;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services
{
    public class ServiceModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<ISettingService<DatabaseData>, DatabaseSettingService>();
            appCore.RegisterType<ISettingService<ThemeData>, ThemeSettingService>();

            return Task.CompletedTask;
        }
    }
}