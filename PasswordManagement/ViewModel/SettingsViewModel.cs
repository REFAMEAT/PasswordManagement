using System.Collections.Generic;
using System.Windows.Input;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;
using PasswordManagement.Services.Implementations;
using PasswordManagement.Settings;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class SettingsViewModel : NotifyPropertyChanged
    {
        private ICommand buttonCommandApplySettings;
        private readonly List<ISetting> settings;

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