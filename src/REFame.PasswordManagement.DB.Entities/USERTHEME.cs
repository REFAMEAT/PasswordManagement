using System.ComponentModel.DataAnnotations;
using REFame.PasswordManagement.DB.Entities.Base;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace REFame.PasswordManagement.DB.Entities
{
    public class USERTHEME : GenerateTable
    {
        [Key]
        public string UTID { get; set; }

        public USERDATA User { get; set; }

        /// <summary>
        ///     UI-Language
        /// </summary>
        public string UTLANGUAGE { get; set; }

        /// <summary>
        ///     The applications primary color
        /// </summary>
        public string UTPRIMARYCOLOR { get; set; }

        /// <summary>
        ///     The applications secondary color
        /// </summary>
        public string UTSECONDARYCOLOR { get; set; }

        /// <summary>
        ///     The application-theme
        /// </summary>
        public static string UTTHEME { get; set; }
    }
}