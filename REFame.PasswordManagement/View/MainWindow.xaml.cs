using System;
using System.Reflection;
using System.Windows;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.App.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IDataManager<PasswordData> dataManager = null)
        {
            InitializeComponent();

            Title = string.Format(Loc.MainWindow_ctor_Title, typeof(MainWindow).Assembly.GetName().Version);

            DataContext = new MainViewModel(dataManager);

        }
    }
}