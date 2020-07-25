using System.Collections.Generic;
using System.Windows.Input;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;
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
            settings = new List<ISetting>
            {
                Style,
                Database
            };

            settings.ForEach(x => x.Load());
        }

        public DatabaseSetting Database { get; set; } = new DatabaseSetting();
        public StyleSetting Style { get; set; } = new StyleSetting();

        public ICommand ButtonCommandApplySettings => buttonCommandApplySettings ??= new Command(DoApplySettings);

        private void DoApplySettings(object obj)
        {
            Database.Password = (obj as Settings).password.Password;

            settings.ForEach(x => x.Save());

            (obj as Settings).Close();
        }
    }
}