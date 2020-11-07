using System;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class ThemeSettingService : ISettingService<ThemeData>
    {
        public ThemeData Load()
        {
            return JsonHelper<ThemeData>.GetData(new ThemeData
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
        }

        public void OnSaved(EventArgs args)
        {
            Saved?.Invoke(this, args);
        }

        public event EventHandler Saved;
    }
}