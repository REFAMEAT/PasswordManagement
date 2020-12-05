using System.Windows;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.App.View
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