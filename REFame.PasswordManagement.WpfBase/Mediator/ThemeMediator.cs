using System;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.WpfBase.Mediator
{
    public static class ThemeMediator
    {
        public static event EventHandler<ThemeEventArgs> ChangeThemeRequested;

        public static void RequestChangeTheme(ThemeData newTheme)
        {
            ChangeThemeRequested?.Invoke(newTheme, new ThemeEventArgs(newTheme));
        }
    }

    public class ThemeEventArgs : EventArgs
    {
        public readonly ThemeData NewTheme;

        public ThemeEventArgs(ThemeData newTheme)
        {
            NewTheme = newTheme;
        }
    }
}