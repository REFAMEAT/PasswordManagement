using System;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Settings.Tests
{
    public class StyleSettingServiceMock : ISettingService<ThemeData>
    {
        private ThemeData mockData = new ThemeData
        {
            Language = Language.English,
            PrimaryColor = "Blue",
            SecondaryColor = "Blue",
            Theme = BaseTheme.Light
        };

        public ThemeData Load()
        {
            return mockData;
        }

        public void Save(ThemeData data)
        {
            mockData = data;
        }

        public void OnSaved(EventArgs args)
        {
            Saved?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Saved;
    }
}