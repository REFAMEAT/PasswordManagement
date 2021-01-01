using System.Windows;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.Settings.SettingFactories.Contracts;
using REFame.PasswordManagement.Settings.UI;
using REFame.PasswordManagement.Settings.UI.Tabs;

namespace REFame.PasswordManagement.Settings
{
    public class Setting : ISetting
    {
        public void Open()
        {
            SettingsView settings = GetSettingsWindow();

            settings.AddSetting<IDatabaseSettingsFactory, DatabaseSettings>();
            settings.AddSetting<IThemeSettingsFactory, ThemeSettings>();

            var settingsViewModelFactory = new SettingsViewModelFactory();
            settingsViewModelFactory.ReportMediators(settings.Mediators);
            settings.SetViewModel(settingsViewModelFactory);

            settings.Show();
        }

        private static SettingsView GetSettingsWindow()
        {
            var settings = new SettingsView
            {
                Owner = Application.Current.MainWindow, 
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            settings.Resources.MergedDictionaries.Clear();

            foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                settings.Resources.MergedDictionaries.Add(dictionary);
            }

            return settings;
        }
    }
}