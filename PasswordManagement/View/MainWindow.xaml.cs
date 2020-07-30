using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;
using PasswordManagement.ViewModel;
using System.Windows;

namespace PasswordManagement.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindow(IDataManager<PasswordData> dataManager = null)
        {
            InitializeComponent();

            if (!(dataManager is null))
            {
                DataContext = new MainViewModel(dataManager);

            }
        }
    }
}