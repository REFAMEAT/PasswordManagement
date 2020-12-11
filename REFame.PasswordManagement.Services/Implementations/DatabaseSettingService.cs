using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class DatabaseSettingService : ISettingService<DatabaseData>
    {
        public async Task<DatabaseData> Load()
        {
            return await PWCore.CurrentCore
                .GetRegisteredType<IConfigurationFactory<DatabaseData>>()
                .SetPath()
                .Create()
                .LoadAsync();
        }

        public async Task Save(DatabaseData data)
        {
            await PWCore.CurrentCore
                .GetRegisteredType<IConfigurationFactory<DatabaseData>>()
                .SetPath()
                .Create()
                .WriteAsync(data);
        }
    }
}