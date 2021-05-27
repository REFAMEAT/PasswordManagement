using System.Threading.Tasks;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class ThemeSettingService : ISettingService<ThemeData>
    {
        private readonly IConfiguration<ThemeData> config;

        public ThemeSettingService(IConfigurationFactory<ThemeData> config)
        {
            this.config = config
                .SetPath()
                .Create();
        }

        public async Task<ThemeData> Load()
        {
            return await config.LoadAsync();
        }

        public async Task Save(ThemeData data)
        {
            await config.WriteAsync(data);
        }
    }
}