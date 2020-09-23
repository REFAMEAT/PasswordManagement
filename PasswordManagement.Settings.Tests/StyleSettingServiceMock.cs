using System;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Model.Enums;
using PasswordManagement.Model.Setting;
using PasswordManagement.Services.Interfaces;

namespace PasswordManagement.Settings.Tests
{
    public class StyleSettingServiceMock : ISettingService<ThemeData>
    {
        private ThemeData mockData = new ThemeData()
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