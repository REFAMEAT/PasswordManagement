using System;
using System.Globalization;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.AppCore
{
    class CoreInformation : ICoreInformation
    {
        public CultureInfo AppCultureInfo { get; internal set; }
        public ThemeData ThemeData { get; internal set; }
        public ILoginHandler LoginHandler { get; internal set; }
        public Type MainWindow { get; internal set; }
        public Type MainViewModel { get; internal set; }
    }
}
