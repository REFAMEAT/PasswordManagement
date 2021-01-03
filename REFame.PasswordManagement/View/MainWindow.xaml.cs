using System.Windows;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Localization;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = string.Format(Loc.MainWindow_ctor_Title, typeof(MainWindow).Assembly.GetName().Version);
        }
    }
}