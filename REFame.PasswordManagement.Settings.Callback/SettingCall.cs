using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Windows;
using System.Windows.Input;
using REFame.PasswordManagement.Settings.SettingFactories;
using REFame.PasswordManagement.Settings.UI.Tabs;

namespace REFame.PasswordManagement.Settings.Call
{
    public class SettingCall
    {
        public static void Open()
        {
            var settings = GetSettingsWindow();

            settings.AddSetting<DatabaseSettingsFactory, DatabaseSettings>();
            settings.AddSetting<ThemeSettingsFactory, ThemeSettings>();

            var settingsViewModelFactory = new SettingsViewModelFactory();
            settingsViewModelFactory.ReportMediators(settings.Mediators);
            settings.SetViewModel(settingsViewModelFactory);

            settings.Show();
        }

        private static UI.Settings GetSettingsWindow()
        {
            var settings = new UI.Settings();
            settings.Owner = Application.Current.MainWindow;
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            
            settings.Resources.MergedDictionaries.Clear();

            foreach (var dictionary in Application.Current.Resources.MergedDictionaries)
            {
                settings.Resources.MergedDictionaries.Add(dictionary);
            }

            return settings;
        }
    }
}