using System.Windows;

namespace PasswordManagement.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.Windows[0]; 
            Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }
    }
}
