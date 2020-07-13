using System;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Theme;

namespace PasswordManagement.Backend.Xml
{
    [Serializable]
    public class ThemeData
    {
        public Language Language { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public BaseTheme Theme { get; set; }
    }
}