using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using ITheme = REFame.PasswordManagement.Model.Interfaces.ITheme;

namespace REFame.PasswordManagement.Services.Implementations
{
    public class ThemeSettingService : ISettingService<ITheme>
    {
        public async Task<ITheme> Load()
        {
            return await JsonHelper<ThemeData>.GetDataAsync(new ThemeData
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                SecondaryColor = "Blue",
                Theme = BaseTheme.Light
            });
        }

        public async Task Save(ITheme data)
        {
            await JsonHelper<ThemeData>.WriteDataAsync(data as ThemeData);
        }
    }
}