using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class ThemeSettingService : ISettingService<ThemeData>
    {
        public async Task<ThemeData> Load()
        {
            return await PWCore.CurrentCore
                .GetRegisteredType<IConfigurationFactory<ThemeData>>()
                .SetPath()
                .Create()
                .LoadAsync();
        }

        public async Task Save(ThemeData data)
        {
            await PWCore.CurrentCore
                .GetRegisteredType<IConfigurationFactory<ThemeData>>()
                .SetPath()
                .Create()
                .WriteAsync(data);
        }
    }
}