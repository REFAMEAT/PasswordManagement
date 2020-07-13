using System.Windows;

namespace PasswordManagement.View
{
    /// <summary>
    ///     Interaction logic for Add.xaml
    /// </summary>
    public partial class AddPassword : Window
    {
        public AddPassword()
        {
            InitializeComponent();
            Loaded += AddPassword_Loaded;
        }

        private void AddPassword_Loaded(object sender, RoutedEventArgs e)
        {
            // Pose the Window in the middle-front of the MainWindow
            Application curApp = Application.Current;
            Window mainWindow = curApp.Windows[0];
            Left = mainWindow.Left + (mainWindow.Width - ActualWidth) / 2;
            Top = mainWindow.Top + (mainWindow.Height - ActualHeight) / 2;
        }
    }
}