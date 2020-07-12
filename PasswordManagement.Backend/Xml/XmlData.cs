using System;
using System.Drawing;
using MaterialDesignThemes.Wpf;

namespace PasswordManagement.Backend.Xml
{
    [Serializable]
    public class XmlData
    {
        public Language Language { get; set; }

        public Color PrimaryColor { get; set; }

        public Color SecondaryColor { get; set; }

        public BaseTheme Theme { get; set; }
    }
}