using System.Collections.Generic;
using System.Windows.Input;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Services.Implementations;
using REFame.PasswordManagement.Settings;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class SettingsViewModel : WpfBase.BindableBase
    {
        private readonly List<ISetting> settings;
        private ICommand buttonCommandApplySettings;

        public SettingsViewModel()
        {
            var themeSettings = new ThemeSettingService();
            themeSettings.Saved += (sender, args) => UiHelper.AdjustApplicationStyle(themeSettings.Load());

            settings = new List<ISetting>
            {
                new StyleSetting(themeSettings),
                Database
            };

            settings.ForEach(x => x.Load());
        }

        public DatabaseSetting Database { get; set; } = new DatabaseSetting(new DatabaseSettingService());

        public ICommand ButtonCommandApplySettings => buttonCommandApplySettings ??= new Command(DoApplySettings);

        private void DoApplySettings(object obj)
        {
            Database.Password = (obj as View.Settings).password.Password;

            settings.ForEach(x => x.Save());

            (obj as View.Settings).Close();
        }
    }
}