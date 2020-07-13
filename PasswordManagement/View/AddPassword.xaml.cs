using System.Windows;

namespace PasswordManagement.View
{
    /// <summary>
    /// Interaction logic for Add.xaml
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
            Application curApp = Application.Current;
            Window mainWindow = curApp.Windows[0];
            Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }
    }
}

