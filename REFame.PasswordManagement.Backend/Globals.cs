using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Backend
{
    public static class Globals
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

        public static bool UseDatabase { get; set; }
        public static string CurrentUserId { get; set; }
        public static string UserHash { get; set; }
        public static ThemeData DefaultTheme { get; }
        public static DatabaseData DefaultDb { get; }
    }
}