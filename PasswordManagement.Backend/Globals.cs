using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;
using PasswordManagement.File;

namespace PasswordManagement.Backend
{
    public class Globals
    {
        static Globals()
        {
            DefaultTheme = new ThemeData()
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
                UseDatabase = false,
            };
        }

        public static string CurrentUserId { get; set; }
        public static string UserHash { get; set; }
        public static ThemeData DefaultTheme { get; set; }
        public static DatabaseData DefaultDb { get; set; }
    }
}