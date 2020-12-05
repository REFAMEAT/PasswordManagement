using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class ThemeSettingService : ISettingService<ThemeData>
    {
        public async Task<ThemeData> Load()
        {
            return await JsonHelper<ThemeData>.GetDataAsync(new ThemeData
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                SecondaryColor = "Blue",
                Theme = BaseTheme.Light
            });
        }

        public async Task Save(ThemeData data)
        {
            await JsonHelper<ThemeData>.WriteDataAsync(data);
        }

        public void OnSaved(EventArgs args)
        {
            Saved?.Invoke(this, args);
        }

        public event EventHandler Saved;
    }
}