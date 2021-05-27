using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class DatabaseSettingService : ISettingService<DatabaseData>
    {
        private IConfiguration<DatabaseData> config;

        public DatabaseSettingService(IConfigurationFactory<DatabaseData> databaseDataConfigurationFactory)
        {
            this.config = databaseDataConfigurationFactory
                .SetPath()
                .Create();
        }

        public async Task<DatabaseData> Load()
        {
            return await config.LoadAsync();
        }

        public async Task Save(DatabaseData data)
        {
            await config.WriteAsync(data);
        }
    }
}