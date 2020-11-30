using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Backend
{
    public static class ThemeStart
    {
        public static AppCore StartThemes(this AppCore app, out ThemeData theme)
        {
            theme = JsonHelper<ThemeData>.GetData(Globals.DefaultTheme);

            if (theme.Theme != BaseTheme.Inherit || theme.PrimaryColor != null)
            {
                return app;
            }

            theme = new ThemeData
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                Theme = BaseTheme.Light
            };
            JsonHelper<ThemeData>.WriteData(theme);

            return app;
        }
    }
}