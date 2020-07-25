using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;
using PasswordManagement.File;

namespace PasswordManagement.Backend.Json
{
    public static class ThemeStart
    {
        internal static AppCore StartThemes(this AppCore app, out ThemeData theme)
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