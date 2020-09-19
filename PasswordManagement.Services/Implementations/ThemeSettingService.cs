using System;
using MaterialDesignThemes.Wpf;
using PasswordManagement.File.Config;
using PasswordManagement.Model.Enums;
using PasswordManagement.Model.Setting;
using PasswordManagement.Services.Interfaces;

namespace PasswordManagement.Services.Implementations
{
    public class ThemeSettingService : ISettingService<ThemeData>
    {
        public ThemeData Load()
        {
            return JsonHelper<ThemeData>.GetData(new ThemeData()
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                SecondaryColor = "Blue",
                Theme = BaseTheme.Light
            });
        }

        public void Save(ThemeData data)
        {
            JsonHelper<ThemeData>.WriteData(data);

            Saved?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Saved;
    }
}