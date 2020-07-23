using System.Windows;
using PasswordManagement.Backend;
using PasswordManagement.Backend.Binary;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Xml;
using PasswordManagement.Logging;

namespace PasswordManagement
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static string loginPw;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += (o, args) => Logger.Current.Error(args.Exception);

            AppCore.StartCore()
                .SetupBinaries()
                .StartThemes(out ThemeData data)
                .StartUpUi(data)
                .Login(out bool success)
                .StartMain(success);

            if (!success)
            {
                Shutdown(1);
            } 
        }
    }
}