using System;
using PasswordManagement.Model.Interfaces;
using PasswordManagement.Model.Setting;
using PasswordManagement.Services.Interfaces;

namespace PasswordManagement.Settings
{
    public class DatabaseSetting : ISetting
    {
        private readonly ISettingService<DatabaseData> databaseSettingService;

        public DatabaseSetting(ISettingService<DatabaseData> databaseSettingService)
        {
            this.databaseSettingService = databaseSettingService;
        }

        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool UseDatabase { get; set; }

        public void Load()
        {
            DatabaseData data = databaseSettingService.Load();
            DatabaseName = data.DatabaseName;
            ServerName = data.ServerName;
            Password = data.Password;
            Username = data.Username;
            IntegratedSecurity = data.IntegratedSecurity;
            UseDatabase = data.UseDatabase;

        }

        public void Save()
        {
            databaseSettingService.Save(new DatabaseData()
            {
                DatabaseName = DatabaseName ??= "",
                Password = Password ??= "",
                ServerName = ServerName ??= "",
                Username = Username ??= "",
                IntegratedSecurity = IntegratedSecurity,
                UseDatabase = UseDatabase
            });
            databaseSettingService.OnSaved(EventArgs.Empty);
        }
    }
}