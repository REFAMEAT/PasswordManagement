using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;
using PasswordManagement.Model;
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
            settings.ForEach(x => x.Save());
        }
    }
}