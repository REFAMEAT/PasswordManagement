using System;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Settings.Tests
{
    public class DatabaseSettingServiceMock : ISettingService<DatabaseData>
    {
        public static DatabaseData mockData = new DatabaseData
        {
            Username = Mocks.UserName,
            Password = Mocks.Password,
            DatabaseName = Mocks.DatabaseName,
            ServerName = Mocks.ServerName,
            IntegratedSecurity = Mocks.IntegratedSecurity,
            UseDatabase = Mocks.UseDatabase
        };

        public DatabaseData Load()
        {
            return mockData;
        }

        public void Save(DatabaseData data)
        {
            mockData = data;
        }

        public void OnSaved(EventArgs args)
        {
            Saved?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Saved;

        public class Mocks
        {
            public const bool IntegratedSecurity = false;
            public const bool UseDatabase = true;
            public const string DatabaseName = "testDB";
            public const string ServerName = "testDB";
            public const string UserName = "testDB";
            public const string Password = "testDB";
        }
    }
}