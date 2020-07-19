using System;
using MaterialDesignThemes.Wpf;

namespace PasswordManagement.Backend.Xml
{
    [Serializable]
    public class XmlData
    {
        public Language Language { get; set; }

        public string PrimaryColor { get; set; }

        public string SecondaryColor { get; set; }

        public BaseTheme Theme { get; set; }
    }
}