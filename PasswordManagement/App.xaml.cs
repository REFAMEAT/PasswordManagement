using System.Windows;
using PasswordManagement.Backend;
using PasswordManagement.Backend.Binary;
using PasswordManagement.Logging;
using PasswordManagement.Model;
using PasswordManagement.Model.Setting;

namespace PasswordManagement
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        internal static string loginPw;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += (o, args) => Logger.Current.Error(args.Exception);

            AppCore.StartCore()
                .SetupBinaries()
                .StartThemes(out ThemeData data)
                .StartUpUi(data)
                .Login()
                .StartMain();
        }
    }
}