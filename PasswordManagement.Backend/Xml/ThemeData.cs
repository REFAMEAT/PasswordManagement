using System;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Settings;

namespace PasswordManagement.Backend.Xml
{
    /// <summary>
    /// Model for the UI-Theme Data
    /// </summary>
    [Serializable]
    [Obsolete("Will be ported to Database: Use USERTHEME")]
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