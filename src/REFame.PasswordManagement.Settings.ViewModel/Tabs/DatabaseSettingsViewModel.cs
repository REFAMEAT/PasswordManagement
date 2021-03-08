using System;
using System.Collections.Generic;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Implementations;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.ViewModel.Tabs
{
    public class DatabaseSettingsViewModel : BindableBase
    {
        private SettingMediator settingMediator;
        private readonly ISettingService<DatabaseData> settings;

        public DatabaseSettingsViewModel(ISettingService<DatabaseData> settings = null)
        {
            this.settings = settings ?? new DatabaseSettingService();
            DatabaseData data = this.settings.Load().Result;

            DatabaseName = data.DatabaseName;
            ServerName = data.ServerName;
            Password = data.Password;
            Username = data.Username;
            IntegratedSecurity = data.IntegratedSecurity;
            UseDatabase = data.UseDatabase;
        }

        public SettingMediator SettingMediator
        {
            get => settingMediator;
            set
            {
                settingMediator = value;
                settingMediator.SaveRequested += SettingMediatorOnSaveRequested;
            }
        }

        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool IntegratedSecurity { get; set; }
        public bool UseDatabase { get; set; }

        public async void SettingMediatorOnSaveRequested(object sender, EventArgs e)
        {
            await settings.Save(new DatabaseData
            {
                DatabaseName = DatabaseName,
                IntegratedSecurity = IntegratedSecurity,
                Password = Password,
                ServerName = ServerName,
                UseDatabase = UseDatabase,
                Username = Username
            });
        }
    }
}