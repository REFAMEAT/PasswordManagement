using System;
using System.Windows;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : PwmWindow
    {
        public MainWindow(IUserInfo userInfo)
        {
            InitializeComponent();
            Title = string.Format(Loc.MainWindow_ctor_Title, 
                typeof(MainWindow).Assembly.GetName().Version, 
                userInfo.User.FullName);
            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}