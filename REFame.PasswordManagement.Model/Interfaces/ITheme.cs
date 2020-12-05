using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Model.Enums;

namespace REFame.PasswordManagement.Model.Interfaces
{
    public interface ITheme
    {
        /// <summary>
        ///     UI-Language
        /// </summary>
        Language Language { get; set; }

        /// <summary>
        ///     The applications primary color
        /// </summary>
        string PrimaryColor { get; set; }

        /// <summary>
        ///     The applications secondary color
        /// </summary>
        string SecondaryColor { get; set; }

        /// <summary>
        ///     The application-theme
        /// </summary>
        BaseTheme Theme { get; set; }
    }
}