using MaterialDesignThemes.Wpf;
using PasswordManagement.Model.Enums;
using PasswordManagement.Model.Setting;

namespace PasswordManagement.Backend
{
    internal static class Globals
    {
        static Globals()
        {
            DefaultTheme = new ThemeData
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                SecondaryColor = "Blue",
                Theme = BaseTheme.Light
            };

            DefaultDb = new DatabaseData
            {
                DatabaseName = "localhost",
                IntegratedSecurity = true,
                Username = "",
                Password = "",
                ServerName = "",
                UseDatabase = false
            };
        }

        internal static bool UseDatabase { get; set; }
        internal static string CurrentUserId { get; set; }
        internal static string UserHash { get; set; }
        internal static ThemeData DefaultTheme { get; }
        internal static DatabaseData DefaultDb { get; }
    }
}