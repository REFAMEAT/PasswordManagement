using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Backend
{
    public static class Globals
    {
        public static bool UseDatabase { get; set; }
        public static string CurrentUserId { get; set; }
        public static string UserHash { get; set; }
    }
}