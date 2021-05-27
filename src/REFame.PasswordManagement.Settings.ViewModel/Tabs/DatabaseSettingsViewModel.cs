using System;
using System.Xaml;
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

        public DatabaseSettingsViewModel(ISettingService<DatabaseData> settings)
        {
            this.settings = settings;
            DatabaseData data = this.settings.Load().Result;

            DatabaseName = data.DatabaseName;
            ServerName = data.ServerName;
            Password = data.Password;
            Username = data.Username;
            IntegratedSecurity = data.IntegratedSecurity;
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
        public DataBaseType Type { get; set; }

        public async void SettingMediatorOnSaveRequested(object sender, EventArgs e)
        {
            await settings.Save(new DatabaseData
            {
                DatabaseName = DatabaseName,
                IntegratedSecurity = IntegratedSecurity,
                Password = Password,
                ServerName = ServerName,
                Username = Username
            });
        }
    }
}