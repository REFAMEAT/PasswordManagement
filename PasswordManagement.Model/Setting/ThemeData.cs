using MaterialDesignThemes.Wpf;
using PasswordManagement.Model.Enums;

namespace PasswordManagement.Model.Setting
{
    /// <summary>
    /// Model for the UI-Theme Data
    /// </summary>
    public class ThemeData
    {
        /// <summary>
        /// UI-Language
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// The applications primary color
        /// </summary>
        public string PrimaryColor { get; set; }

        /// <summary>
        /// The applications secondary color
        /// </summary>
        public string SecondaryColor { get; set; }

        /// <summary>
        /// The application-theme
        /// </summary>
        public BaseTheme Theme { get; set; }
    }
}