﻿using System.Windows;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.App
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static string loginPw;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += (o, args) => Logger.Current.Get().Error(args.Exception);

            AppCore.StartCore()
                .SetupBinaries()
                .StartThemes(out ThemeData data)
                .StartUpUi(data)
                .Login()
                .StartMain();
        }
    }
}