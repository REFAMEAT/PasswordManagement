using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Login;
using REFame.PasswordManagement.Services;
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
                .RegisterModule<ServiceModule>()
                .RegisterModule<LocalizationModule>()
                .RegisterModule<UISetup>()
                .RegisterModule<LoginModule>();

            PWCore.CurrentCore.Run();
            WpfCore.Current.Login(PWCore.CurrentCore);

            WpfCore.Current.RegisterMainWindow<MainWindow>();
        }
    }
}