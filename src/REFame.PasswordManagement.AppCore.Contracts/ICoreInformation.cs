 using System;
using System.Globalization;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.AppCore.Contracts
{
    public interface ICoreInformation
    {
        CultureInfo AppCultureInfo { get; }

        ThemeData ThemeData { get; }

        ILoginHandler LoginHandler { get; }

        Type MainWindow { get; }

        Type MainViewModel { get; }
    }
}