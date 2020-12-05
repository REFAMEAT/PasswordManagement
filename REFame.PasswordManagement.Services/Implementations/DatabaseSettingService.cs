using System.Threading.Tasks;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class DatabaseSettingService : ISettingService<DatabaseData>
    {
        public async Task<DatabaseData> Load()
        {
            return await JsonHelper<DatabaseData>.GetDataAsync(new DatabaseData
            {
                DatabaseName = "localhost",
                IntegratedSecurity = true,
                Username = "",
                Password = "",
                ServerName = "",
                UseDatabase = false
            });
        }

        public async Task Save(DatabaseData data)
        {
            await JsonHelper<DatabaseData>.WriteDataAsync(data);
        }
    }
}