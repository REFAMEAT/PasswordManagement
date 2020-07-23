﻿using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;

namespace PasswordManagement.Backend.Json
{
    public static class ThemeStart
    {
        public static AppCore StartThemes(this AppCore app, out ThemeData theme)
        {
            theme = JsonHelper.GetData();

            if (theme.Theme != BaseTheme.Inherit || theme.PrimaryColor != null)
            {
                return app;
            }

            theme = new ThemeData
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                Theme = BaseTheme.Light
            };
            JsonHelper.WriteData(theme);

            return app;
        }
    }
}