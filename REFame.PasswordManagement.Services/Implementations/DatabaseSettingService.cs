using System;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class DatabaseSettingService : ISettingService<DatabaseData>
    {
        public DatabaseData Load()
        {
            return JsonHelper<DatabaseData>.GetData(new DatabaseData
            {
                DatabaseName = "localhost",
                IntegratedSecurity = true,
                Username = "",
                Password = "",
                ServerName = "",
                UseDatabase = false
            });
        }

        public void Save(DatabaseData data)
        {
            JsonHelper<DatabaseData>.WriteData(data);
        }

        public void OnSaved(EventArgs args)
        {
            Saved?.Invoke(this, args);
        }

        public event EventHandler Saved;
    }
}