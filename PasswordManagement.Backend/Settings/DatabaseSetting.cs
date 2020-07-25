using PasswordManagement.Backend.Json;

namespace PasswordManagement.Backend.Settings
{
    public class DatabaseSetting : ISetting
    {
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool UseDatabase { get; set; }

        public void Load()
        {
            DatabaseData data = JsonHelper<DatabaseData>.GetData(new DatabaseData());
            DatabaseName = data.DatabaseName;
            ServerName = data.ServerName;
            Password = data.Password;
            Username = data.Username;
            IntegratedSecurity = data.IntegratedSecurity;
            UseDatabase = data.UseDatabase;

        }

        public void Save()
        {
            JsonHelper<DatabaseData>.WriteData(new DatabaseData()
            {
                DatabaseName = DatabaseName,
                Password = Password,
                ServerName = ServerName,
                Username = Username,
                IntegratedSecurity = IntegratedSecurity,
                UseDatabase = UseDatabase
            });
        }

        public static bool Database() => JsonHelper<DatabaseData>.GetData(Globals.DefaultDb).UseDatabase;
        public static bool WindowsLogin() => JsonHelper<DatabaseData>.GetData(Globals.DefaultDb).IntegratedSecurity;
    }
}