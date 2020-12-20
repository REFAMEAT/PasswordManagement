using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Database;
using REFame.PasswordManagement.File.Module;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Login;
using REFame.PasswordManagement.Services;
using REFame.PasswordManagement.Update;
using REFame.PasswordManagement.WpfBase.Localization;

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

            PWCore.Create();
            PWCore.CurrentCore
                .RegisterModule<UpdateModule>()
                .RegisterModule<ServiceModule>()
                .RegisterModule<FileModule>()
                .RegisterModule<LocalizationModule>()
                .RegisterModule<UISetup>()
                .RegisterModule<DatbaseModule>()
                .RegisterModule<LoginModule>()
                .RegisterModule<DataModule>();

            PWCore.CurrentCore.Run();
            bool success = WpfCore.Current.Login(PWCore.CurrentCore);

            if (success)
            {
                WpfCore.Current.RegisterMainWindow<MainWindow>(); 
            }
        }
    }
}