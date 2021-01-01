using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Model.Enums;

namespace REFame.PasswordManagement.Model.Setting
{
    /// <summary>
    ///     Model for the UI-Theme Data
    /// </summary>
    public class ThemeData
    {
        /// <summary>
        ///     UI-Language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///     The applications primary color
        /// </summary>
        public string PrimaryColor { get; set; }

        /// <summary>
        ///     The applications secondary color
        /// </summary>
        public string SecondaryColor { get; set; }

        /// <summary>
        ///     The application-theme
        /// </summary>
        public BaseTheme Theme { get; set; }
    }
}